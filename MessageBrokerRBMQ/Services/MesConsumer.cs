using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MessageBrokerRBMQ.Services;

public class MesConsumer : IMesConsumer{
    public void Recive<T>(){
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            UserName = "khoi",
            Password = "khoideptrai",
            VirtualHost = "/",
        };
        var conn = factory.CreateConnection();
        using var channel = conn.CreateModel();

        channel.QueueDeclare("abc", durable: true, exclusive: true);

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received  += (model, eventArgs) => {
            var body = eventArgs.Body.ToArray();

            var message = Encoding.UTF8.GetString(body);

        };

        channel.BasicConsume("abc", true, consumer);
    }
}