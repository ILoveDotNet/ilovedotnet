using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using SharedModels;
using Web.Models;

namespace Web.Shared;

public class SearchBase : ComponentBase, IAsyncDisposable
{
    private IJSObjectReference? module;
    private short _selectedListItemIndex = -1;
    private string _searchText = string.Empty;

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

    [Parameter, EditorRequired] public EventCallback<bool> ToggleNonSearchItems { get; set; }
    [CascadingParameter] public bool SmallDevice { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender) 
        {
            module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/search.js");
        }
    }

    protected List<ContentMetaData> FilteredContents => TableOfContents.Contents
        .Where(content => SearchText.Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                                    .Any(searchTerm => content.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)))
        .ToList();

    protected async Task Search()
    {
        if (SmallDevice)
        {
            HideNonSearchItems = true;
        }

        await ToggleNonSearchItem();
    }

    protected async Task ExitSearch()
    {
        if (!SmallDevice) return;

        HideNonSearchItems = false;

        await ToggleNonSearchItem();
    }

    protected async Task KeyUpHandler(KeyboardEventArgs eventArgs)
    {
        await KeyDownHandler(eventArgs);
    }

    protected async Task KeyDownHandler(KeyboardEventArgs eventArgs)
    {
        switch (eventArgs.Key)
        {
            case "Enter":
                OnEnterKey();
                break;
            case "ArrowDown":
                await SelectNextItem(+1);
                break;
            case "ArrowUp":
                await SelectNextItem(-1);
                break;
            case "Escape":
                await SearchInput.FocusAsync();
                break;
            default:
                break;
        }
    }

    protected async Task FocusOutHandler()
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

    private async Task SelectNextItem(short increment)
    {
        if (FilteredContents.Count == 0)
            return;
        _selectedListItemIndex = (short)Math.Max(0, Math.Min(FilteredContents.Count - 1, _selectedListItemIndex + increment));
        var elementIdToFocus = $"{_componentId}_{_selectedListItemIndex}";
        await module!.InvokeVoidAsync("focusElement", elementIdToFocus);
    }

    private async Task ToggleNonSearchItem()
    {
        await ToggleNonSearchItems.InvokeAsync(HideNonSearchItems);
    }

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        if (module is not null)
        {
            await module.DisposeAsync();
        }
    }
}
