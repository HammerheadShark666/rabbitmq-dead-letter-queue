using RabbitMq.DeadletterQueue;
using RabbitMQ.Client;

Console.WriteLine("Rabbit Dead Letter Queue Example");

RabbitMQConnection rabbitMQConnection = new RabbitMQConnection(new ConnectionFactory());
IModel channel = rabbitMQConnection.CreateModel();
 
Consumer1 consumer1 = new Consumer1(channel);
consumer1.SetConsumer();

Consumer2 consumer2 = new Consumer2(channel);
consumer2.SetConsumer();

Producer producer = new Producer(channel);

producer.Publish("Test Publish Item");

Thread.Sleep(10000);