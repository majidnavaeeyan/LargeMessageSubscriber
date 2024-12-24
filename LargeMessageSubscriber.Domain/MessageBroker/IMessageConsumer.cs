using LargeMessageSubscriber.Domain.DTOs;

namespace LargeMessageSubscriber.Domain.MessageBroker
{
  public interface IMessageConsumer
  {
    Task<IEnumerable<Point>> ConsumeMessageAsync();
  }
}
