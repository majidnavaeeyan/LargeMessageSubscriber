using LargeMessageSubscriber.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LargeMessageSubscriber.Application
{
  public static class ServiceCollectionExtensions
  {
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
      // Register Services
      services.AddScoped<IPointService, PointService>();
      services.AddScoped<IConfigurationService, ConfigurationService>();

      return services;
    }
  }
}
