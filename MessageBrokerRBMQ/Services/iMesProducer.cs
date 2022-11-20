namespace MessageBrokerRBMQ.Services;


public interface IMesProducer
{
    public void SendingMes<T>(T mes);
}