using Microsoft.Extensions.DependencyInjection;

namespace LargeMessageSubscriber.Infrastructure.MessageBroker
{
  public static class ServiceCollectionExtensions
  {
    public static IServiceCollection AddMessageBrokerInfrastructure(this IServiceCollection services)
    {
      //// Register DbContext
      //services.AddDbContext<ApplicationDbContext>(options =>          options.UseSqlServer("YourConnectionString"));

      // Register Repositories
      //services.AddScoped<IPointRepository, PointRepository>();

      return services;
    }
  }
}
