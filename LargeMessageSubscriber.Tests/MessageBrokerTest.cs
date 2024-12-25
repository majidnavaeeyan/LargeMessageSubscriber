using LargeMessageSubscriber.Application;
using LargeMessageSubscriber.Domain.DTOs;
using LargeMessageSubscriber.Domain.MessageBroker;
using LargeMessageSubscriber.Domain.Repository;
using Moq;

namespace LargeMessageSubscriber.Tests
{
  public class MessageBrokerTest
  {
    [Fact]
    public void LoadTestTheMessageBroker()
    {
      for (int i = 0; i < 5; i++)
      {
        var data = MakeDate();
        var pointRepository = new Mock<IPointRepository>();
        var messageConsumer = new Mock<IMessageConsumer>();
        var messageProducer = new Mock<IMessageProducer>();

        var service = new PointService(pointRepository.Object, messageConsumer.Object, messageProducer.Object);

        var result = service.EnqueuMessageToMessageBrokerAsync(data);
      }

      Assert.True(true);
    }

    private DateTime RandomDay()
    {
      var gen = new Random();
      var start = new DateTime(2024, 12, 20);
      var range = (DateTime.Today - start).Days;
      return start.AddDays(gen.Next(range));
    }

    private List<Point> MakeDate()
    {
      var myDictionary = new Dictionary<string, Domain.DTOs.Point>();

      for (int i = 0; i < 5000; i++)
      {
        var name = string.Join("", Enumerable.Repeat(0, 5).Select(n => (char)new Random().Next(97, 122)));
        var item = new Domain.DTOs.Point { Name = name, Timestamp = RandomDay(), Value = new Random().Next(1, 100) };
        myDictionary.TryAdd(name, item);
      }

      var model = myDictionary.Select(q => q.Value).ToList();

      return model;
    }
  }
}