using Coffeeshop.Domain.Interfaces;
using StackExchange.Redis;

namespace Coffeeshop.Infrastructure.Redis;

/// <summary>
/// Example of using Redis as a queue
/// </summary>
internal class ProcessQueue : IProcessQueue
{
    public ProcessQueue(RedisContext redisContext)
    {
        _redisContext = redisContext;
    }

    private readonly RedisContext _redisContext;
    private readonly static RedisKey REDIS_KEY = new RedisKey("ORDER_QUEUE");
   
    public async Task Schedule(long id)
    {
        var value = new RedisValue(id.ToString());
        await _redisContext.Database.ListLeftPushAsync(REDIS_KEY, value, When.Always);
    }

    public async Task<long?> TakeNext()
    {
        if (_redisContext.Database.ListLength(REDIS_KEY) > 0)
        {
            var value = await _redisContext.Database.ListRightPopAsync(REDIS_KEY, 1);
            if (long.TryParse(value.FirstOrDefault(), out long id))
            {
                return id;
            }
        }

        return default(long?);
    }
}
