using LargeMessageSubscriber.Domain.DTOs;

namespace LargeMessageSubscriber.Domain.MessageBroker
{
  public interface IMessageProducer
  {
    Task ProduceMessageAsync(IEnumerable<Point> model);
  }
}
