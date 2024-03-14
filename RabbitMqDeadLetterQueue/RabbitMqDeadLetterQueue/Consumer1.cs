using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMq.DeadletterQueue;

public class Consumer1(IModel channel)
{
    private IModel _channel { get; set; } = channel;
    private string _queue = "demo-queue-1";

    public void SetConsumer()
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += ReceivedEvent;

        _channel.BasicConsume(queue: _queue, autoAck: false, consumer: consumer);
    }  

    public async void ReceivedEvent(object sender, BasicDeliverEventArgs ea)
    {
        try
        {
            var message = Encoding.UTF8.GetString(ea.Body.Span);
            var deserializedMessage = JsonConvert.DeserializeObject<object>(message);

            Console.WriteLine(String.Format("{0} - {1}", _queue, deserializedMessage));

            throw new Exception("Exception 1");

            _channel.BasicAck(ea.DeliveryTag, false);

            return;
        }         
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        _channel.BasicNack(ea.DeliveryTag, false, false); 
    } 
}