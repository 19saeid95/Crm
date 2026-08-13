using Crm.Application.Interfaces.Authentication;
using StackExchange.Redis;

namespace Crm.Infrastructure.Authentication;

public sealed class RedisRefreshTokenService(
    IConnectionMultiplexer redis)
    : IRefreshTokenService
{
    private const string KeyPrefix = "crm:refresh-token:";

    private static readonly TimeSpan RefreshTokenLifetime =
        TimeSpan.FromDays(7);

    public async Task<string> CreateAsync(
        long userId,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var refreshToken = Guid.NewGuid().ToString("N");

        var database = redis.GetDatabase();

        await database.StringSetAsync(
            GetKey(refreshToken),
            userId.ToString(),
            RefreshTokenLifetime);

        return refreshToken;
    }

    public async Task<long?> ValidateAsync(
        string refreshToken,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var database = redis.GetDatabase();

        var value = await database.StringGetAsync(
            GetKey(refreshToken));

        if (!value.HasValue)
            return null;

        if (!long.TryParse(value.ToString(), out var userId))
            return null;

        return userId;
    }

    public async Task RevokeAsync(
        string refreshToken,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var database = redis.GetDatabase();

        await database.KeyDeleteAsync(
            GetKey(refreshToken));
    }

    private static RedisKey GetKey(string refreshToken)
    {
        return $"{KeyPrefix}{refreshToken}";
    }
}