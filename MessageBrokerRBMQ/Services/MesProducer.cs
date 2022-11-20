using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace MessageBrokerRBMQ.Services;

public class MesProducer : IMesProducer
{
    public void SendingMes<T>(T mes)
    {
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

        var jsonString = JsonSerializer.Serialize(mes);

        var body = Encoding.UTF8.GetBytes(jsonString);


        channel.BasicPublish("", "abc", body: body);
    }
}