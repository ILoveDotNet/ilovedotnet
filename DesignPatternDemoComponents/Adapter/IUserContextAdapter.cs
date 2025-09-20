namespace DesignPatternDemoComponents.Adapter;

/// <summary>
/// Adapter interface that provides a simplified, cross-platform way to access user context information.
/// This interface abstracts platform-specific implementations (ASP.NET Core, MAUI, etc.)
/// </summary>
public interface IUserContextAdapter
{
  /// <summary>
  /// Gets the current user's unique identifier
  /// </summary>
  string GetCurrentUserId();

  /// <summary>
  /// Gets the current user's primary role
  /// </summary>
  string GetCurrentUserRole();
}
