namespace SharedModels;
public class AppStateDemo
{
    public event Action OnChange = default!;
    public readonly List<string> messages1 = [];
    public readonly List<string> messages2 = [];

    public void SendMessageToChat1(string message)
    {
        messages1.Add(message);
        NotifyStateChanged();
    }

    public void SendMessageToChat2(string message)
    {
        messages2.Add(message);
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
