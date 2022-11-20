namespace MessageBrokerRBMQ.Services;


public interface IMesConsumer
{
    public void Recive<T>();
}