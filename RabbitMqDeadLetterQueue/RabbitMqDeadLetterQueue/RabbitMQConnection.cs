using RabbitMQ.Client;

namespace RabbitMq.DeadletterQueue;

public class RabbitMQConnection
{ 
    private IConnection _connection;

    private const string Host = "rabbitmq";
    private const string Exchange = "demo-dead-letter-exchange";
    private const string ExchangeType = "fanout";
    private const string Username = "AdminUser";
    private const string Password = "BorderC66#3";

    public RabbitMQConnection(IConnectionFactory connectionFactory)
    {
        _connection = connectionFactory.CreateConnection();           
    }
    public IModel CreateModel()
    {
        return _connection.CreateModel();
    }   

    public RabbitMQSettings GetRabbitMQSettings()
    {
        return new RabbitMQSettings(Host, Exchange, ExchangeType, Username, Password); 
    }

    public record RabbitMQSettings(string HostName, string ExchangeName, string ExchangeType, string UserName, string Password);
}