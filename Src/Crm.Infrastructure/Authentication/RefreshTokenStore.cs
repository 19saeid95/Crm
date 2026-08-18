using Crm.Application.Contracts.Authentication;
using StackExchange.Redis;

namespace Crm.Infrastructure.Authentication;

public class RefreshTokenStore(IConnectionMultiplexer redis) : IRefreshTokenStore
{
    private const string KeyPrefix = "refresh-token:";
    public async Task<long?> GetUserIdAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        var tokenHash = RefreshTokenHasher.Hash(refreshToken);
        var database = redis.GetDatabase();
        var key = KeyPrefix + tokenHash;
        var value = await database.StringGetAsync(key);
        if (!value.HasValue)
            return null;

        return long.Parse(value!);
    }

    public async Task RemoveAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        var tokenHash =RefreshTokenHasher.Hash(refreshToken);
        var database = redis.GetDatabase();
        var key = KeyPrefix + tokenHash;
        await database.KeyDeleteAsync(key);
    }

    public async Task StoreAsync(string refreshToken, long userId, DateTime expiresAtUtc, CancellationToken cancellationToken = default)
    {
        var tokenHash = RefreshTokenHasher.Hash(refreshToken);
        var database = redis.GetDatabase();
        var key = KeyPrefix + tokenHash;
        var ttl = expiresAtUtc - DateTime.UtcNow;
        await database.StringSetAsync(key, userId, ttl);
    }
}
