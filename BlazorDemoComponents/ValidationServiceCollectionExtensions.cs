using Microsoft.Extensions.DependencyInjection;

namespace BlazorDemoComponents;

public static class ValidationServiceCollectionExtensions
{
  public static IServiceCollection AddBlazorDemoValidation(this IServiceCollection services)
  {
    services.AddValidation();
    return services;
  }
}
