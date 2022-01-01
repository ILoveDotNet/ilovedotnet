using Microsoft.AspNetCore.Components;
using Web.Models;

namespace Web.Shared;

public class ThemeBase : ComponentBase
{
    internal string Icon { get; private set; } = "brightness-up";
    internal bool MenuCollapsed { get; private set; } = true;

    [Inject] private AppState AppState { get; set; } = default!;

    internal void ToggleMenu()
    {
        MenuCollapsed = !MenuCollapsed;
    }

    internal void FocusOutHandler()
    {
        MenuCollapsed = true;
    }

    internal void SetTheme(DisplayMode mode) 
    {
        Icon = mode switch
        {
            DisplayMode.Light => "brightness-up",
            DisplayMode.Dark => "moon",
            _ => "device-desktop"
        };

        AppState.DisplayModeChanged(mode == DisplayMode.Dark);
    }
}
