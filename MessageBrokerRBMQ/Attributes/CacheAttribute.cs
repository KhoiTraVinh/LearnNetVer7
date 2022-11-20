using MessageBrokerRBMQ.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MessageBrokerRBMQ.Attributes;


public class CacheAttribute : Attribute, IAsyncActionFilter
{

    private readonly int _ttl;

    public CacheAttribute(int ttl){
        _ttl = ttl;
    }
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var cache = context.HttpContext.RequestServices.GetRequiredService<IRedisCache>();

        if(cache == null){
            await next();
            return;
        }

        var cacheService = context.HttpContext.RequestServices.GetRequiredService<IRedisCache>();
        var key = context.HttpContext.Request.ToString();
        var val = await cacheService.getCache(key);

        if(string.IsNullOrEmpty(val)){
            var Result = new ContentResult
            {
                Content = val,
                ContentType = "application/json",
                StatusCode = 200
            };
            context.Result = Result;
            return;
        }

        var exContext = await next();

        if(exContext.Result is OkObjectResult objectResult){
            await cacheService.setCache(key, objectResult.Value, TimeSpan.FromSeconds(_ttl));
        }
    }
}