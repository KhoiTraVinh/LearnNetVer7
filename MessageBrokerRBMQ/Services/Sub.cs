using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MessageBrokerRBMQ.Services;

public class Sub : ISub{
    public void Recive<T>()
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            UserName = "khoi",
            Password = "khoideptrai",
            VirtualHost = "/",
        };
        // create connect
        var conn = factory.CreateConnection();
        // create channel
        using var channel = conn.CreateModel();

        // create exchange
        channel.ExchangeDeclare("test", type: "fanout", durable: true, autoDelete: false);

        // create queue
        // exclusive -> nếu bạn hủy đăng kí thì rbmq sẽ tự động xóa quere để tránh lãng phí queue
        channel.QueueDeclare("xyz", exclusive: true);

        //binding là từ exchange sang queue
        channel.QueueBind("xyz", "test", "");

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received  += (model, eventArgs) => {
            var body = eventArgs.Body.ToArray();

            var message = Encoding.UTF8.GetString(body);


        };
        channel.BasicConsume("xyz", true, consumer);

        
    }

}