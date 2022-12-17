using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace MessageBrokerRBMQ.Services;

public class MesProducer : IMesProducer
{
    public IModel CreateQueue(){
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

        return channel;
    }
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
        //thêm tll trên create queue -> nhưng phải thêm luôn bên consumer -> nên xài thêm trên producer
        var args = new Dictionary<string, object>();
        args.Add("x-message-ttl", 5000);
        channel.QueueDeclare("testttl", durable: true, exclusive: false, autoDelete: false, args);
        var jsonString = JsonSerializer.Serialize(mes);

        var body = Encoding.UTF8.GetBytes(jsonString);
        // thêm ttl trên producer
        // IBasicProperties props = channel.CreateBasicProperties();
        // props.ContentType = "text/plain";
        // props.DeliveryMode = 2;
        // props.Expiration = "5000";

        channel.BasicPublish("", "testttl", null, body);
    }
}