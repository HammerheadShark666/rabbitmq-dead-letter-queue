using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMq.DeadletterQueue;

public class Producer(IModel channel)
{
    private IModel _channel { get; set; } = channel;
  
    public void Publish(object publishModel)
    {
        var message = JsonConvert.SerializeObject(publishModel);
        var body = Encoding.UTF8.GetBytes(message);

        IBasicProperties properties = _channel.CreateBasicProperties();

        properties.Persistent = true;
        properties.DeliveryMode = 2;

        _channel.ConfirmSelect();
        _channel.BasicPublish(exchange: "demo-dead-letter-exchange",
            routingKey: "demo-queue", mandatory: true,
            basicProperties: properties, body: body);

        _channel.WaitForConfirmsOrDie();
        _channel.ConfirmSelect();
    }
}