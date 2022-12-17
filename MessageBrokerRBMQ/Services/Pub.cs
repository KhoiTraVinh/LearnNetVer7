using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MessageBrokerRBMQ.Services;

public class Pub : IPub{
    public void Send<T>(T mes){
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            UserName = "khoi",
            Password = "khoideptrai",
            VirtualHost = "/",
        };
        var conn = factory.CreateConnection();
        using var channel = conn.CreateModel();
        channel.ExchangeDeclare("test", type: "fanout", durable: true, autoDelete: false);
        var jsonString = JsonSerializer.Serialize(mes);

        var body = Encoding.UTF8.GetBytes(jsonString);
        //public cái exchange lên
        channel.BasicPublish("test", "", null, body);
    }
}