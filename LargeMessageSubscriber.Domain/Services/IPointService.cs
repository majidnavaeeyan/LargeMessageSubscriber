using LargeMessageSubscriber.Domain.DTOs;

namespace LargeMessageSubscriber.Domain.Services
{
  public interface IPointService
  {
    Task InsertAsync(IEnumerable<Point> model);
    Task InsertRecievedMessagesToDbAsync();
    Task EnqueuMessageToMessageBrokerAsync(List<Point> model);
  }
}
