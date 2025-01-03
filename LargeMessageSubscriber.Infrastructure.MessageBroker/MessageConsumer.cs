﻿using LargeMessageSubscriber.Domain.DTOs;
using LargeMessageSubscriber.Domain.MessageBroker;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace LargeMessageSubscriber.Infrastructure.MessageBroker
{
  public class MessageConsumer : IMessageConsumer
  {
    private readonly IConnection _connection;
    private readonly IChannel _channel;

    public MessageConsumer(IConfiguration configuration)
    {
      var address = configuration.GetSection("MessageBroker:Address").Value;

      var factory = new ConnectionFactory() { HostName = address };
      _connection = factory.CreateConnectionAsync().Result;
      _channel = _connection.CreateChannelAsync().Result;
      _channel.QueueDeclareAsync("LargeMessageSubscriberQueue", true, false, false);
    }

    public async Task<IEnumerable<Point>> ConsumeMessageAsync()
    {
      var data = await _channel.BasicGetAsync("LargeMessageSubscriberQueue", false);

      if (data is null)
        return new List<Point>();

      var body = data.Body.ToArray();
      var message = Encoding.UTF8.GetString(body);
      var result = JsonConvert.DeserializeObject<List<Point>>(message);


      await _channel.BasicAckAsync(deliveryTag: data.DeliveryTag, multiple: false);

      return result;
    }
  }
}