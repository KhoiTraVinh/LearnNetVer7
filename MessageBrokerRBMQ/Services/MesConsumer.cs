using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MessageBrokerRBMQ.Services;

public class MesConsumer : IMesConsumer{
    public void Recive<T>(){
        //create connect
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            UserName = "khoi",
            Password = "khoideptrai",
            VirtualHost = "/",
        };
        //create connect
        var conn = factory.CreateConnection();
        //create channel
        using var channel = conn.CreateModel();
        //create name queue
        //if dont have name -> auto create name queue
        var args = new Dictionary<string, object>();
        args.Add("x-message-ttl", 5000);
        channel.QueueDeclare("testttl", durable: true, exclusive: false, autoDelete: false, args);
        //durable -> if turn off your queue, data still hold
        var consumer = new EventingBasicConsumer(channel);

        consumer.Received  += (model, eventArgs) => {
            var body = eventArgs.Body.ToArray();

            var message = Encoding.UTF8.GetString(body);


        };
        // ack là cách để consumer thông báo với rbmq là nó đã nhận được mes đó chưa 
        // nếu nhận được thì báo là true để rbmq xóa mes đó đi
        // nếu false thì sẽ đẩy mes đó sang consumer khác đến khi nào true thì thôi
        // nếu cứ để mes tồn tại mãi thì sẽ gây tràn queue
        // autoAck = noAck but set = false didn't change to unacked
        // autoAck set = true -> = set noAck = true
        // nếu có hơn 1 consumer nó sẽ chia đều mes tới từng consumer
        channel.BasicConsume("testttl", true, consumer);
    }
}