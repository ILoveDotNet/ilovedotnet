using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Web.Models;

namespace Web.Shared;

public class SearchBase : ComponentBase, IAsyncDisposable
{
    const ushort SMALLDEVICEWIDTH = 640;

    private IJSObjectReference? module;
    private DotNetObjectReference<SearchBase>? objRef;
    private ushort viewPortWidth;
    private short _selectedListItemIndex = -1;

    protected bool SmallDevice;
    protected bool HideNonSearchItems;
    protected Guid _componentId = Guid.NewGuid();
    protected string SearchText = string.Empty;
    protected bool ShowSuggestions => !string.IsNullOrWhiteSpace(SearchText) && FilteredContents.Count > 0;
    protected List<ContentMetaData> FilteredContents => TableOfContents.Contents
        .Where(content => content.Title.Contains(SearchText, StringComparison.OrdinalIgnoreCase)).ToList();

    [Parameter, EditorRequired] public EventCallback<bool> ToggleNonSearchItems { get; set; }
    [Inject] private TableOfContents TableOfContents { get; set; } = default!;
    [Inject] private IJSRuntime JSRuntime { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        objRef = DotNetObjectReference.Create(this);

        module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/search.js");

        viewPortWidth = await module.InvokeAsync<ushort>("getViewPortWidth", objRef, false);

        SmallDevice = viewPortWidth < SMALLDEVICEWIDTH;
    }

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

    private async Task ToggleNonSearchItem()
    {
        await ToggleNonSearchItems.InvokeAsync(HideNonSearchItems);
    }

    protected async Task KeyUpHandler(KeyboardEventArgs eventArgs)
    {
        await KeyDownHandler(eventArgs);
    }

    protected async Task KeyDownHandler(KeyboardEventArgs eventArgs)
    {
        switch (eventArgs.Key)
        {
            //case "Enter":
            //    await OnEnterKey();
            //    break;
            case "ArrowDown":
                await SelectNextItem(+1);
                break;
            case "ArrowUp":
                await SelectNextItem(-1);
                break;
            default:

                break;
        }
    }

    private async Task SelectNextItem(short increment)
    {
        if (FilteredContents.Count == 0)
            return;
        _selectedListItemIndex = (short)Math.Max(0, Math.Min(FilteredContents.Count - 1, _selectedListItemIndex + increment));
        var elementIdToFocus = $"{_componentId}_{_selectedListItemIndex}";
        await module!.InvokeVoidAsync("focusElement", elementIdToFocus);
    }

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        if (objRef is not null && module is not null)
        {
            await module.InvokeAsync<ushort>("getViewPortWidth", objRef, true);
            objRef.Dispose();
            await module.DisposeAsync();
        }
    }

    [JSInvokable]
    public async Task WindowResized(bool smallDevice)
    {
        SmallDevice = smallDevice;
        HideNonSearchItems = false;
        await InvokeAsync(StateHasChanged);
    }
}
