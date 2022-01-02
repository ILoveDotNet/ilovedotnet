using Microsoft.AspNetCore.Components;
using Web.Models;

namespace Web.Shared;

public class ThemeBase : ComponentBase
{
    internal DisplayMode DisplayMode { get; private set; }
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
        DisplayMode = mode;

        AppState.DisplayModeChanged(mode == DisplayMode.Dark);
    }
}
