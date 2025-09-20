namespace DesignPatternDemoComponents.Adapter;

/// <summary>
/// Mock implementation for demo purposes - simulates a user context adapter
/// </summary>
public class BlazorUserContextAdapter : IUserContextAdapter
{
  public string GetCurrentUserId() => "Abdul Rahman";

  public string GetCurrentUserRole() => "Blazor Admin";
}
