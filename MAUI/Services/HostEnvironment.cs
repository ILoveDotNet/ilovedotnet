using SharedModels;

namespace MAUI.Services;
internal class MAUIHostEnvironment : IHostEnvironment
{
  public string Environment { get; set; } = "Local";

  public bool IsDevelopment()
  {
    return string.Equals(Environment, "Development", StringComparison.OrdinalIgnoreCase);
  }

  public bool IsPrerendering()
  {
    return string.Equals(Environment, "Prerendering", StringComparison.OrdinalIgnoreCase);
  }

  public bool IsProduction()
  {
    return string.Equals(Environment, "Production", StringComparison.OrdinalIgnoreCase);
  }
}
