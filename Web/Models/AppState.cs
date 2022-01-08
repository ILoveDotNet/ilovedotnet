namespace Web.Models;

internal class AppState
{
    internal event Action OnChange = default!;

    private void NotifyStateChanged() => OnChange?.Invoke();
}
