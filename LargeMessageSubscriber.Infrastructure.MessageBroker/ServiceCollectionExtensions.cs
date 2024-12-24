using LargeMessageSubscriber.Domain.MessageBroker;
using Microsoft.Extensions.DependencyInjection;

namespace LargeMessageSubscriber.Infrastructure.MessageBroker
{
  public static class ServiceCollectionExtensions
  {
    public static IServiceCollection AddMessageBrokerInfrastructure(this IServiceCollection services)
    {
      //Register Repositories
      services.AddScoped<IMessageConsumer, MessageConsumer>();
      services.AddScoped<IMessageProducer, MessageProducer>();

      return services;
    }
  }
}
