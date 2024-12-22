using Microsoft.Extensions.DependencyInjection;

namespace LargeMessageSubscriber.Infrastructure
{
  public static class ServiceCollectionExtensions
  {
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
      //// Register DbContext
      //services.AddDbContext<ApplicationDbContext>(options =>          options.UseSqlServer("YourConnectionString"));

      //// Register Repositories
      //services.AddScoped<IOrderRepository, OrderRepository>();

      return services;
    }
  }
}
