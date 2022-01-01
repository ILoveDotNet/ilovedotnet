using Microsoft.AspNetCore.Components;
using Web.Models;

namespace Web.Shared;

public class ThemeBase : ComponentBase
{
    protected string Icon { get; set; } = "brightness-up";
    protected bool MenuCollapsed { get; set; } = true;

    protected void ToggleMenu()
    {
        MenuCollapsed = !MenuCollapsed;
    }

    protected void FocusOutHandler()
    {
        MenuCollapsed = true;
    }

    protected void SetTheme(UITheme theme) 
    {
        Icon = theme switch
        {
            UITheme.Light => "brightness-up",
            UITheme.Dark => "moon",
            _ => "device-desktop"
        };
    }
}
