using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Web.Models;

namespace Web.Shared;

public class ThemeBase : ComponentBase, IAsyncDisposable
{
    private IJSObjectReference? module;

    internal DisplayMode DisplayMode { get; private set; }
    internal bool MenuCollapsed { get; private set; } = true;

    [Inject] private ILocalStorageService LocalStorage { get; set; } = default!;
    [Inject] private IJSRuntime JSRuntime { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        DisplayMode = await LocalStorage.ContainKeyAsync($"{nameof(DisplayMode)}") &&
                      Enum.TryParse(typeof(DisplayMode), await LocalStorage.GetItemAsStringAsync($"{nameof(DisplayMode)}"), true, out var displayMode)
                      ? (DisplayMode)displayMode!
                      : DisplayMode.System;

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

        module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/displaymode.js");

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
