using Microsoft.Extensions.DependencyInjection;

namespace LargeMessageSubscriber.Application
{
  public static class ServiceCollectionExtensions
  {
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
      // Register Services
      //services.AddScoped<OrderService>();

      return services;
    }
  }
}
