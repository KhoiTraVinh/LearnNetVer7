using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Serialization;
using StackExchange.Redis;
using Newtonsoft.Json;

namespace MessageBrokerRBMQ.Services;


public class RedisCache : IRedisCache
{
    private readonly IDistributedCache _cacheDb;


    public RedisCache(){
        var redis = ConnectionMultiplexer.Connect("localhost:6379");
        _cacheDb = (IDistributedCache)redis.GetDatabase();
    }
    public async Task<string> getCache(string key)
    {
        var val = await _cacheDb.GetStringAsync(key);
        return string.IsNullOrEmpty(val) ? null : val;
    }

    public async Task setCache(string key, object val, TimeSpan ttl)
    {
        if(val == null){
            return;
        }
        var serializerResponse = JsonConvert.SerializeObject(val, new JsonSerializerSettings(){
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        });

        await _cacheDb.SetStringAsync(key, serializerResponse, new DistributedCacheEntryOptions{
            AbsoluteExpirationRelativeToNow = ttl
        });
        
    }
}