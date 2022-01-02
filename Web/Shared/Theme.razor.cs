using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Web.Models;

namespace Web.Shared;

public class ThemeBase : ComponentBase
{
    internal DisplayMode DisplayMode { get; private set; }
    internal bool MenuCollapsed { get; private set; } = true;

    [Inject] private AppState AppState { get; set; } = default!;
    [Inject] private ILocalStorageService LocalStorage { get; set; } = default!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            DisplayMode = await LocalStorage.ContainKeyAsync($"{nameof(DisplayMode)}") ?
                          await LocalStorage.GetItemAsync<DisplayMode>($"{nameof(DisplayMode)}") :
                          DisplayMode.Light;

            AppState.DisplayModeChanged(DisplayMode == DisplayMode.Dark);

            await InvokeAsync(StateHasChanged);
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

        AppState.DisplayModeChanged(mode == DisplayMode.Dark);

        await LocalStorage.SetItemAsync($"{nameof(DisplayMode)}", mode.ToString());
    }
}
