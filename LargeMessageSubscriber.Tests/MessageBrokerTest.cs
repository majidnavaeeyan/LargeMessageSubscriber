using LargeMessageSubscriber.Domain.DTOs;
using LargeMessageSubscriber.Domain.Services;
using Moq;
using Newtonsoft.Json;

namespace LargeMessageSubscriber.Tests
{
  public class MessageBrokerTest
  {
    [Fact]
    public void LoadTestTheMessageBroker()
    {
      var _pointService = new Mock<IPointService>();

      for (int i = 0; i < 10; i++)
      {
        var data = new Mock<List<Point>>(MakeDate()).Object;
        _pointService.Setup(q => q.EnqueuMessageToMessageBrokerAsync(data)).Returns(Task.CompletedTask);
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

    private List<Domain.DTOs.Point> MakeDate()
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