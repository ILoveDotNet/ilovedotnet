using DesignPatternDemoComponents.Adapter;

/// <summary>
/// Mock implementation for demo purposes - simulates a user context adapter
/// </summary>
public class MAUIContextAdapter : IUserContextAdapter
{
    public string GetCurrentUserId() => "Abdul Rahman";

    public string GetCurrentUserRole() => "MAUI Admin";
}