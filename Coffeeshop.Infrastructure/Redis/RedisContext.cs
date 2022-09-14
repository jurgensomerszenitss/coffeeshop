using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Coffeeshop.Infrastructure.Redis;

public class RedisContext 
{
    public RedisContext(IOptions<RedisOptions> options)
    {
        var redis = ConnectionMultiplexer.Connect(options.Value.ConnectionString);
        Database = redis.GetDatabase();
    }

    public IDatabase Database { get; init;  }
}
