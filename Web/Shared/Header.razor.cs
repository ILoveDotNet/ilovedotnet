using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Web.Shared;

public class HeaderBase : ComponentBase, IAsyncDisposable
{
    const ushort SMALLDEVICEWIDTH = 640;
    
    private IJSObjectReference? module;
    private ushort viewPortWidth;

    protected bool SmallDevice;
    protected bool HideNonSearchItems;

    [Inject] private IJSRuntime JSRuntime { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/viewport.js");

        viewPortWidth = await module.InvokeAsync<ushort>("getViewPortWidth");

        SmallDevice = viewPortWidth < SMALLDEVICEWIDTH;
    }

    protected void Search() 
    {
        if (SmallDevice) 
        {
            HideNonSearchItems = true;
        }
    }

    protected void ExitSearch()
    {
        if (!SmallDevice) return;
        
        HideNonSearchItems = false;
    }

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        if (module is not null)
        {
            await module.DisposeAsync();
        }
    }
}
