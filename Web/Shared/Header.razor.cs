using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Web.Shared;

public class HeaderBase : ComponentBase, IAsyncDisposable
{
    const ushort SMALLDEVICEWIDTH = 640;

    private IJSObjectReference? module;
    private DotNetObjectReference<HeaderBase>? objRef;
    private ushort viewPortWidth;

    protected bool SmallDevice;
    protected bool HideNonSearchItems;

    [Inject] private IJSRuntime JSRuntime { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        objRef = DotNetObjectReference.Create(this);

        module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/viewport.js");

        viewPortWidth = await module.InvokeAsync<ushort>("getViewPortWidth", objRef, false);

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
