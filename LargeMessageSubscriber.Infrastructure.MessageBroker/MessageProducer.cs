using LargeMessageSubscriber.Domain.DTOs;
using LargeMessageSubscriber.Domain.MessageBroker;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace LargeMessageSubscriber.Infrastructure.MessageBroker
{
  public class MessageProducer : IMessageProducer
  {
    private readonly IConnection _connection;
    private readonly IChannel _channel;

    public MessageProducer(IConfiguration configuration)
    {
      var address = configuration.GetSection("MessageBroker:Address").Value;

      var factory = new ConnectionFactory() { HostName = address };
      _connection = factory.CreateConnectionAsync().Result;
      _channel = _connection.CreateChannelAsync().Result;
      _channel.QueueDeclareAsync("LargeMessageSubscriberQueue", true, false, false);
    }

    public async Task ProduceMessageAsync(IEnumerable<Point> model)
    {
      await _channel.QueueDeclareAsync("LargeMessageSubscriberQueue", true, false, false);
      var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));

      await _channel.BasicPublishAsync(string.Empty, "LargeMessageSubscriberQueue", body);
    }
  }
}
