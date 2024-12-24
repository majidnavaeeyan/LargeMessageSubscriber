using LargeMessageSubscriber.Domain.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace LargeMessageSubscriber.Infrastructure.DataAccess
{
  public static class ServiceCollectionExtensions
  {
    public static IServiceCollection AddDataAccessInfrastructure(this IServiceCollection services)
    {
      // Register Repositories
      services.AddTransient<IPointRepository, PointRepository>();

      return services;
    }
  }
}
