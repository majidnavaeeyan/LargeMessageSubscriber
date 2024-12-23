using LargeMessageSubscriber.Domain.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace LargeMessageSubscriber.Infrastructure.DataAccess
{
  public static class ServiceCollectionExtensions
  {
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
      // Register Repositories
      services.AddScoped<IPointRepository, PointRepository>();

      return services;
    }
  }
}
