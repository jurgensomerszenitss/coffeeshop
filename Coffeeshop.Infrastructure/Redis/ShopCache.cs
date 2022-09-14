using Coffeeshop.Domain.Interfaces;
using Coffeeshop.Domain.Models;
using StackExchange.Redis;

namespace Coffeeshop.Infrastructure.Redis;

internal class ShopCache : IShopCache
{
    public ShopCache(RedisContext redisContext)
    {
        _redisContext = redisContext;
    }

    private readonly RedisContext _redisContext;
    private readonly static RedisKey REDIS_KEY = new RedisKey("SHOP");

    public async Task<ShopStatus> GetStatus()
    {
        var value = await _redisContext.Database.StringGetAsync(REDIS_KEY);
        if (Enum.TryParse(typeof(ShopStatus), value, out var status))
        {
            return (ShopStatus) status;
        }

        return ShopStatus.Closed;
    }

    public async Task SetStatus(ShopStatus status)
    {
        await _redisContext.Database.StringSetAsync(REDIS_KEY, new RedisValue(status.ToString()));
    }
}
