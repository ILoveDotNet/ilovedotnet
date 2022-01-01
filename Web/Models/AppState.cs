namespace Web.Models;

internal class AppState
{
    internal bool DarkMode { get; private set; }

    internal event Action OnChange;

    private void NotifyStateChanged() => OnChange?.Invoke();

    internal void DisplayModeChanged(bool darkMode)
    {
        DarkMode = darkMode;

        NotifyStateChanged();
    }
}
