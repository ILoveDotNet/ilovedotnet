namespace Web.Models;

internal class AppState
{
    internal event Action OnChange;

    private void NotifyStateChanged() => OnChange?.Invoke();
}
