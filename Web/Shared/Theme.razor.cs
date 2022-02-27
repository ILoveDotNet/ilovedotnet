using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SharedModels;

namespace Web.Shared;

public class ThemeBase : ComponentBase, IAsyncDisposable
{
    private IJSObjectReference? module;

    internal DisplayMode DisplayMode { get; private set; }
    internal bool MenuCollapsed { get; private set; } = true;

    [Inject] private IJSRuntime JSRuntime { get; set; } = default!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender) 
        {
            module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/displaymode.js");

            DisplayMode = Enum.TryParse(typeof(DisplayMode), await module.InvokeAsync<string>("getDisplayMode"), true, out var displayMode)
                          ? (DisplayMode)displayMode!
                          : DisplayMode.System;

            StateHasChanged();
        }
    }

    internal void ToggleMenu()
    {
        MenuCollapsed = !MenuCollapsed;
    }

    internal void FocusOutHandler()
    {
        MenuCollapsed = true;
    }

    internal async Task SetTheme(DisplayMode mode)
    {
        DisplayMode = mode;

        await OnDisplayModeChanged();
    }

    private async ValueTask OnDisplayModeChanged()
    {
        if (module is not null)
        {
            await module.InvokeVoidAsync("onDisplayModeChanged", $"{DisplayMode.ToString().ToLower()}");
        }
    }

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        if (module is not null)
        {
            await module.DisposeAsync();
        }
    }
}
