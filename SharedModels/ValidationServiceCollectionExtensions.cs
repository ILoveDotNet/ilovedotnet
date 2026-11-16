using Microsoft.Extensions.DependencyInjection;

namespace SharedModels;

public static class ValidationServiceCollectionExtensions
{
  public static IServiceCollection AddSharedModelValidation(this IServiceCollection services)
  {
    services.AddValidation();
    return services;
  }
}
