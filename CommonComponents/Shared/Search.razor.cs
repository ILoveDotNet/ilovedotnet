using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using CommonComponents.Services;
using SharedModels;
using Toolbelt.Blazor.HotKeys2;

namespace CommonComponents.Shared;

public class SearchBase : ComponentBase, IAsyncDisposable
{
  private IJSObjectReference? module;
  private short _selectedListItemIndex = -1;
  private string _searchText = string.Empty;
  private List<ContentMetaData> _filteredContents = [];
  private int _searchVersion;
  private CancellationTokenSource? _searchCancellationTokenSource;
  private readonly CancellationTokenSource _lifetimeCancellationTokenSource = new();
  private readonly CancellationTokenSource _warmUpDelayCancellationTokenSource = new();
  private HotKeysContext? HotKeysContext;
  private bool _disposed;

  protected bool HideNonSearchItems;
  protected Guid _componentId = Guid.NewGuid();
  protected ElementReference SearchInput;
  protected string SearchText
  {
    get { return _searchText; }
    set
    {
      _searchText = value;

      if (value.Length == 0)
      {
        _selectedListItemIndex = -1;
      }
    }
  }
  protected bool ShowSuggestions { get; set; }

  [Inject] private IContentSearchService ContentSearchService { get; set; } = default!;
  [Inject] private IJSRuntime JSRuntime { get; set; } = default!;
  [Inject] private NavigationManager NavigationManager { get; set; } = default!;
  [Inject] private HotKeys HotKeys { get; set; } = default!;

  [Parameter, EditorRequired] public EventCallback<bool> ToggleNonSearchItems { get; set; }
  [CascadingParameter] public bool SmallDevice { get; set; }

  protected override async Task OnAfterRenderAsync(bool firstRender)
  {
    if (firstRender)
    {
      var importedModule = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/search.js");
      if (_disposed)
      {
        await importedModule.DisposeAsync();
        return;
      }

      module = importedModule;
      HotKeysContext = HotKeys.CreateContext()
                              .Add(Key.Slash, () => SearchInput.FocusAsync());
      _ = WarmUpSearchAsync();
    }
  }

  protected IReadOnlyList<ContentMetaData> FilteredContents => _filteredContents;

  protected async Task SearchTextChangedAsync(ChangeEventArgs eventArgs)
  {
    SearchText = eventArgs.Value?.ToString() ?? string.Empty;
    var searchText = SearchText;
    var searchVersion = ++_searchVersion;
    var cancellationTokenSource = new CancellationTokenSource();
    var cancellationToken = cancellationTokenSource.Token;
    var previousCancellationTokenSource = Interlocked.Exchange(ref _searchCancellationTokenSource, cancellationTokenSource);
    if (previousCancellationTokenSource is not null)
    {
      await previousCancellationTokenSource.CancelAsync();
      previousCancellationTokenSource.Dispose();
    }

    if (string.IsNullOrWhiteSpace(SearchText))
    {
      _filteredContents = [];
      ShowSuggestions = false;
      return;
    }

    try
    {
      await Task.Delay(400, cancellationToken);
      var results = await ContentSearchService.SearchAsync(searchText, cancellationToken);
      if (searchVersion != _searchVersion || !searchText.Equals(SearchText, StringComparison.Ordinal))
      {
        return;
      }

      _filteredContents = [.. results];
      ShowSuggestions = _filteredContents.Count > 0;
    }
    catch (OperationCanceledException)
    {
      return;
    }
    catch (Exception exception)
    {
      if (searchVersion == _searchVersion)
      {
        _filteredContents = [];
        ShowSuggestions = false;
      }
      System.Diagnostics.Debug.WriteLine(exception.Message);
    }
  }

  protected async Task FocusHandlerAsync()
  {
    await _warmUpDelayCancellationTokenSource.CancelAsync();
  }

  private async Task WarmUpSearchAsync()
  {
    try
    {
      await Task.Delay(TimeSpan.FromSeconds(1), _warmUpDelayCancellationTokenSource.Token);
    }
    catch (OperationCanceledException)
    {
      if (_lifetimeCancellationTokenSource.IsCancellationRequested)
      {
        return;
      }

      System.Diagnostics.Debug.WriteLine("Vector search warm-up started early because the search box received focus.");
    }

    try
    {
      await ContentSearchService.WarmUpAsync(_lifetimeCancellationTokenSource.Token);
    }
    catch (Exception exception)
    {
      System.Diagnostics.Debug.WriteLine(exception.Message);
    }
  }

  protected async Task SearchAsync()
  {
    if (SmallDevice)
    {
      HideNonSearchItems = true;
    }

    await ToggleNonSearchItemAsync();
  }

  protected async Task ExitSearchAsync()
  {
    if (!SmallDevice) return;

    HideNonSearchItems = false;

    await ToggleNonSearchItemAsync();
  }

  protected async Task KeyUpHandlerAsync(KeyboardEventArgs eventArgs)
  {
    await KeyDownHandlerAsync(eventArgs);
  }

  protected async Task KeyDownHandlerAsync(KeyboardEventArgs eventArgs)
  {
    switch (eventArgs.Key)
    {
      case "Enter":
        OnEnterKey();
        break;
      case "ArrowDown":
        await SelectNextItemAsync(+1);
        break;
      case "ArrowUp":
        await SelectNextItemAsync(-1);
        break;
      case "Escape":
        await SearchInput.FocusAsync();
        break;
      default:
        break;
    }
  }

  protected async Task FocusOutHandlerAsync()
  {
    // to avoid race between mouse click of item in suggestion with OnSelect. without this delay OnSelect
    // is not getting executed when item is clicked from mouse
    await Task.Delay(250, CancellationToken.None);
    ShowSuggestions = _selectedListItemIndex != -1;
    if (!ShowSuggestions)
    {
      _selectedListItemIndex = -1;
    }
  }

  protected void OnSelect(ContentMetaData content)
  {
    SearchText = content.Title.ToLower();
    _filteredContents = [];
    _selectedListItemIndex = -1;
    ShowSuggestions = false;
    NavigationManager.NavigateTo(content.ContentUrl);
  }

  private void OnEnterKey()
  {
    if (!ShowSuggestions)
      return;
    if (FilteredContents.Count == 0)
      return;
    if (_selectedListItemIndex >= 0 && _selectedListItemIndex < FilteredContents.Count)
      OnSelect(FilteredContents[_selectedListItemIndex]);
  }

  private async Task SelectNextItemAsync(short increment)
  {
    if (FilteredContents.Count == 0)
      return;
    _selectedListItemIndex = (short)Math.Max(0, Math.Min(FilteredContents.Count - 1, _selectedListItemIndex + increment));
    var elementIdToFocus = $"{_componentId}_{_selectedListItemIndex}";
    await module!.InvokeVoidAsync("focusElement", elementIdToFocus);
  }

  private async Task ToggleNonSearchItemAsync()
  {
    await ToggleNonSearchItems.InvokeAsync(HideNonSearchItems);
  }

  async ValueTask IAsyncDisposable.DisposeAsync()
  {
    _disposed = true;
    ++_searchVersion;
    await _lifetimeCancellationTokenSource.CancelAsync();
    await _warmUpDelayCancellationTokenSource.CancelAsync();
    _warmUpDelayCancellationTokenSource.Dispose();
    _lifetimeCancellationTokenSource.Dispose();
    var searchCancellationTokenSource = Interlocked.Exchange(ref _searchCancellationTokenSource, null);
    if (searchCancellationTokenSource is not null)
    {
      await searchCancellationTokenSource.CancelAsync();
      searchCancellationTokenSource.Dispose();
    }
    if (HotKeysContext is not null)
    {
      await HotKeysContext.DisposeAsync();
    }

    if (module is not null)
    {
      await module.DisposeAsync();
    }
  }
}
