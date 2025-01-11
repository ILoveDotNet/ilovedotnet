using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using SharedModels;
using Toolbelt.Blazor.HotKeys2;

namespace CommonComponents.Shared;

public class SearchBase : ComponentBase, IAsyncDisposable
{
  private IJSObjectReference? module;
  private short _selectedListItemIndex = -1;
  private string _searchText = string.Empty;
  private HotKeysContext HotKeysContext = default!;

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

      ShowSuggestions = _searchText.Length > 0 && FilteredContents.Count > 0;
    }
  }
  protected bool ShowSuggestions { get; set; }

  [Inject] private TableOfContents TableOfContents { get; set; } = default!;
  [Inject] private IJSRuntime JSRuntime { get; set; } = default!;
  [Inject] private NavigationManager NavigationManager { get; set; } = default!;
  [Inject] private HotKeys HotKeys { get; set; } = default!;

  [Parameter, EditorRequired] public EventCallback<bool> ToggleNonSearchItems { get; set; }
  [CascadingParameter] public bool SmallDevice { get; set; }

  protected override async Task OnAfterRenderAsync(bool firstRender)
  {
    if (firstRender)
    {
      HotKeysContext = HotKeys.CreateContext()
                              .Add(Key.Slash, () => SearchInput.FocusAsync());
      module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/search.js");
    }
  }

  protected List<ContentMetaData> FilteredContents => TableOfContents.Contents
      .Where(content => SearchText.Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                                  .Any(searchTerm => content.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)))
      .ToList();

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
    await Task.Delay(250);
    ShowSuggestions = _selectedListItemIndex != -1;
    if (!ShowSuggestions)
    {
      _selectedListItemIndex = -1;
    }
  }

  protected void OnSelect(ContentMetaData content)
  {
    SearchText = content.Title.ToLower();
    _selectedListItemIndex = -1;
    ShowSuggestions = false;
    NavigationManager.NavigateTo(content.ContentUrl);
  }

  private void OnEnterKey()
  {
    if (ShowSuggestions == false)
      return;
    if (FilteredContents.Count == 0)
      return;
    if (_selectedListItemIndex >= 0 && _selectedListItemIndex < FilteredContents.Count)
      OnSelect(FilteredContents[_selectedListItemIndex]);
    return;
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
    await HotKeysContext.DisposeAsync();

    if (module is not null)
    {
      await module.DisposeAsync();
    }
  }
}
