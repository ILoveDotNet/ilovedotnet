namespace Web.Models;

internal class AppState
{
    public bool ShowNavigation { get; private set; } = true;

    public void ToggleNavigationMenu()
    {
        ShowNavigation = !ShowNavigation;
        NotifyStateChanged();
    }

    public void HideNavigationMenu()
    {
        ShowNavigation = false;
        NotifyStateChanged();
    }

    internal event Action OnChange = default!;

    private void NotifyStateChanged() => OnChange?.Invoke();
}
