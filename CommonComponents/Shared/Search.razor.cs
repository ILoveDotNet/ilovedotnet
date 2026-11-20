using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using CommonComponents.Services;
using SharedModels;
using Toolbelt.Blazor.HotKeys2;

namespace CommonComponents.Shared;

public class SearchBase : ComponentBase, IAsyncDisposable
{
  protected enum SearchState { Idle, Loading, Results, Empty, Failed }

  private IJSObjectReference? _module;
  private HotKeysContext? _hotKeysContext;
  private int _selectedIndex = -1;
  private int _searchVersion;
  private int _focusVersion;
  private bool _hasFocus;
  private bool _focusInputRequested;
  private bool _focusButtonRequested;
  private bool _disposed;
  private CancellationTokenSource? _searchCancellation;
  private readonly CancellationTokenSource _lifetimeCancellation = new();
  private readonly CancellationTokenSource _warmUpDelayCancellation = new();
  private readonly string _componentId = $"search-{Guid.NewGuid():N}";
  private List<ContentMetaData> _filteredContents = [];

  protected ElementReference SearchRoot;
  protected ElementReference SearchInput;
  protected ElementReference SearchButton;
  protected bool HideNonSearchItems;
  protected bool ShowSuggestions;
  protected string SearchText { get; private set; } = string.Empty;
  protected SearchState State { get; private set; }
  protected string SearchError { get; private set; } = string.Empty;
  protected IReadOnlyList<ContentMetaData> FilteredContents => _filteredContents;
  protected string ResultsId => $"{_componentId}-results";
  protected string StatusId => $"{_componentId}-status";
  protected string OptionId(int index) => $"{_componentId}-{index}";
  protected bool IsSelected(int index) => index == _selectedIndex;
  protected string? ActiveOptionId => ShowSuggestions && _selectedIndex >= 0
      && _selectedIndex < _filteredContents.Count ? OptionId(_selectedIndex) : null;
  protected string StatusMessage => !ShowSuggestions ? string.Empty : State switch
  {
    SearchState.Loading => "Searching articles...",
    SearchState.Empty => "No matching tutorials. Try another topic or browse learning paths.",
    SearchState.Failed => SearchError,
    SearchState.Results => $"{_filteredContents.Count} {(_filteredContents.Count == 1 ? "suggestion" : "suggestions")}. Use Up and Down arrow keys to choose, then Enter to open.",
    _ => string.Empty
  };

  [Inject] private IContentSearchService ContentSearchService { get; set; } = default!;
  [Inject] private IJSRuntime JSRuntime { get; set; } = default!;
  [Inject] private NavigationManager NavigationManager { get; set; } = default!;
  [Inject] private HotKeys HotKeys { get; set; } = default!;
  [Inject] private ILogger<SearchBase> Logger { get; set; } = default!;

  [Parameter, EditorRequired] public EventCallback<bool> ToggleNonSearchItems { get; set; }
  [CascadingParameter] public bool SmallDevice { get; set; }

  protected override async Task OnParametersSetAsync()
  {
    if (!SmallDevice && HideNonSearchItems)
    {
      HideNonSearchItems = false;
      await ToggleNonSearchItems.InvokeAsync(false);
    }
  }

  protected override async Task OnAfterRenderAsync(bool firstRender)
  {
    if (firstRender)
    {
      try
      {
        var module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/search.js");
        if (_disposed)
        {
          await module.DisposeAsync();
          return;
        }

        _module = module;
        await module.InvokeVoidAsync("initializeSearch", SearchInput);
        _hotKeysContext = HotKeys.CreateContext().Add(Key.Slash, () => FocusSearchAsync());
      }
      catch (JSException exception)
      {
        Logger.LogWarning(exception, "Unable to initialize search keyboard enhancements.");
      }

      if (!_disposed) _ = WarmUpSearchAsync();
    }

    if (_disposed) return;
    if (_focusInputRequested)
    {
      _focusInputRequested = false;
      await FocusControlAsync(SearchInput);
    }
    if (_focusButtonRequested)
    {
      _focusButtonRequested = false;
      await FocusControlAsync(SearchButton);
    }
  }

  protected Task SearchTextChangedAsync(ChangeEventArgs args)
  {
    SearchText = args.Value?.ToString() ?? string.Empty;
    return RunSearchAsync(debounce: true);
  }

  private async Task RunSearchAsync(bool debounce)
  {
    if (_disposed) return;
    var version = ++_searchVersion;
    var cancellation = CancellationTokenSource.CreateLinkedTokenSource(_lifetimeCancellation.Token);
    cancellation.CancelAfter(TimeSpan.FromSeconds(30));
    var token = cancellation.Token;
    var previous = Interlocked.Exchange(ref _searchCancellation, cancellation);
    if (previous is not null) await previous.CancelAsync();

    _selectedIndex = -1;
    _filteredContents = [];
    SearchError = string.Empty;
    State = string.IsNullOrWhiteSpace(SearchText) ? SearchState.Idle : SearchState.Loading;
    ShowSuggestions = _hasFocus && State != SearchState.Idle;
    var query = SearchText.Trim();
    try
    {
      if (State == SearchState.Idle) return;
      if (debounce) await Task.Delay(400, token);
      var results = await ContentSearchService.SearchAsync(query, token).WaitAsync(token);
      if (_disposed || version != _searchVersion || token.IsCancellationRequested) return;

      _filteredContents = [.. results];
      State = _filteredContents.Count == 0 ? SearchState.Empty : SearchState.Results;
    }
    catch (OperationCanceledException) when (token.IsCancellationRequested)
    {
      if (!_disposed && version == _searchVersion)
      {
        SearchError = "Search took too long. Try again or browse learning paths.";
        State = SearchState.Failed;
        Logger.LogWarning("Article search timed out.");
      }
    }
    catch (Exception exception)
    {
      Logger.LogWarning(exception, "Unable to search articles.");
      if (!_disposed && version == _searchVersion)
      {
        SearchError = "Search couldn't load. Check your connection and try again, or browse learning paths.";
        State = SearchState.Failed;
      }
    }
    finally
    {
      Interlocked.CompareExchange(ref _searchCancellation, null, cancellation);
      cancellation.Dispose();
    }
  }

  protected async Task FocusHandlerAsync()
  {
    if (_disposed) return;
    ++_focusVersion;
    _hasFocus = true;
    ShowSuggestions = State != SearchState.Idle;
    await _warmUpDelayCancellation.CancelAsync();
  }

  protected async Task FocusOutHandlerAsync()
  {
    var version = ++_focusVersion;
    try
    {
      if (_module is not null && await _module.InvokeAsync<bool>("containsFocus", SearchRoot)) return;
    }
    catch (JSException exception)
    {
      Logger.LogWarning(exception, "Unable to determine search focus.");
    }
    if (!_disposed && version == _focusVersion)
    {
      _hasFocus = false;
      CloseSuggestions();
    }
  }

  private async Task WarmUpSearchAsync()
  {
    try
    {
      var lifetimeToken = _lifetimeCancellation.Token;
      try
      {
        await Task.Delay(TimeSpan.FromSeconds(1), _warmUpDelayCancellation.Token);
      }
      catch (OperationCanceledException) when (!lifetimeToken.IsCancellationRequested)
      {
        // Focusing search starts the warm-up immediately.
      }
      if (!_disposed) await ContentSearchService.WarmUpAsync(lifetimeToken);
    }
    catch (OperationCanceledException) when (_lifetimeCancellation.IsCancellationRequested)
    {
    }
    catch (Exception exception)
    {
      Logger.LogWarning(exception, "Search warm-up failed; a search will retry initialization.");
    }
  }

  private async Task FocusSearchAsync()
  {
    if (_disposed) return;
    if (SmallDevice)
    {
      HideNonSearchItems = true;
      await ToggleNonSearchItems.InvokeAsync(true);
    }
    _focusInputRequested = true;
    await InvokeAsync(StateHasChanged);
  }

  protected async Task SearchAsync()
  {
    await FocusSearchAsync();
    if (State != SearchState.Loading && !string.IsNullOrWhiteSpace(SearchText))
      await RunSearchAsync(debounce: false);
  }

  protected async Task RetrySearchAsync()
  {
    if (State == SearchState.Loading) return;
    await FocusControlAsync(SearchInput);
    _hasFocus = true;
    await RunSearchAsync(debounce: false);
  }

  protected async Task ExitSearchAsync()
  {
    await CancelSearchAsync();
    State = SearchState.Idle;
    _filteredContents = [];
    CloseSuggestions();
    HideNonSearchItems = false;
    _focusButtonRequested = true;
    await ToggleNonSearchItems.InvokeAsync(false);
  }

  protected async Task KeyDownHandlerAsync(KeyboardEventArgs args)
  {
    switch (args.Key)
    {
      case "ArrowDown":
        await SelectNextItemAsync(1);
        break;
      case "ArrowUp":
        await SelectNextItemAsync(-1);
        break;
      case "Enter":
        if (ShowSuggestions && _selectedIndex >= 0 && _selectedIndex < _filteredContents.Count)
          await OnSelectAsync(_filteredContents[_selectedIndex]);
        else if (State != SearchState.Loading)
          await SearchAsync();
        break;
      case "Escape":
        if (ShowSuggestions) CloseSuggestions();
        else if (SmallDevice && HideNonSearchItems) await ExitSearchAsync();
        break;
    }
  }

  private async Task SelectNextItemAsync(int increment)
  {
    if (State != SearchState.Results || _filteredContents.Count == 0) return;
    ShowSuggestions = true;
    _selectedIndex = _selectedIndex < 0
        ? increment > 0 ? 0 : _filteredContents.Count - 1
        : Math.Clamp(_selectedIndex + increment, 0, _filteredContents.Count - 1);
    if (_module is not null)
    {
      try
      {
        await _module.InvokeVoidAsync("scrollOptionIntoView", OptionId(_selectedIndex));
      }
      catch (JSException exception)
      {
        Logger.LogWarning(exception, "Unable to scroll the selected search suggestion into view.");
      }
    }
  }

  protected async Task OnSelectAsync(ContentMetaData content)
  {
    await CancelSearchAsync();
    SearchText = content.Title;
    _filteredContents = [];
    State = SearchState.Idle;
    CloseSuggestions();
    HideNonSearchItems = false;
    await ToggleNonSearchItems.InvokeAsync(false);
    NavigationManager.NavigateTo(content.ContentUrl);
  }

  private void CloseSuggestions()
  {
    ShowSuggestions = false;
    _selectedIndex = -1;
  }

  private async Task FocusControlAsync(ElementReference element)
  {
    try
    {
      await element.FocusAsync();
    }
    catch (JSException exception)
    {
      Logger.LogWarning(exception, "Unable to focus a search control.");
    }
  }

  private async Task CancelSearchAsync()
  {
    ++_searchVersion;
    var cancellation = Interlocked.Exchange(ref _searchCancellation, null);
    if (cancellation is not null) await cancellation.CancelAsync();
  }

  async ValueTask IAsyncDisposable.DisposeAsync()
  {
    _disposed = true;
    ++_focusVersion;
    await CancelSearchAsync();
    await _lifetimeCancellation.CancelAsync();
    await _warmUpDelayCancellation.CancelAsync();
    _warmUpDelayCancellation.Dispose();
    _lifetimeCancellation.Dispose();
    if (_hotKeysContext is not null) await _hotKeysContext.DisposeAsync();
    if (_module is not null)
    {
      try
      {
        await _module.InvokeVoidAsync("disposeSearch", SearchInput);
        await _module.DisposeAsync();
      }
      catch (JSDisconnectedException exception)
      {
        Logger.LogDebug(exception, "Search disconnected during disposal.");
      }
    }
  }
}
