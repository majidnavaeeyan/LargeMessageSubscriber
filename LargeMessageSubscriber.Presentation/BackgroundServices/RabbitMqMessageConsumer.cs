using LargeMessageSubscriber.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LargeMessageSubscriber.Presentation.BackgroundServices
{
  public class RabbitMqMessageConsumer : BackgroundService
  {
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public RabbitMqMessageConsumer(IServiceScopeFactory serviceScopeFactory)
    {
      _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
      while (!stoppingToken.IsCancellationRequested)
      {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
          var pointService = scope.ServiceProvider.GetRequiredService<IPointService>();
          await pointService.InsertRecievedMessagesToDbAsync();
        }

        await Task.Delay(1000, stoppingToken);
      }
    }
  }
}
