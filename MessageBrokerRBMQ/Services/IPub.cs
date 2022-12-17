namespace MessageBrokerRBMQ.Services;


public interface IPub
{
    public void Send<T>(T mes);
}