namespace MessageBrokerRBMQ.Services;


public interface IRedisCache{
    Task setCache(string key, object val, TimeSpan ttl);
    Task<string> getCache(string key);

}