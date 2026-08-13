using System.Security.Cryptography;
using System.Text;
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

        var refreshToken = GenerateToken();

        var tokenHash = ComputeHash(refreshToken);

        var database = redis.GetDatabase();

        await database.StringSetAsync(
            GetKey(tokenHash),
            userId.ToString(),
            RefreshTokenLifetime);

        return refreshToken;
    }

    public async Task<long?> ValidateAsync(
        string refreshToken,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var tokenHash = ComputeHash(refreshToken);

        var database = redis.GetDatabase();

        var value = await database.StringGetAsync(
            GetKey(tokenHash));

        if (!value.HasValue)
            return null;

        return long.TryParse(
            value.ToString(),
            out var userId)
            ? userId
            : null;
    }

    public async Task RevokeAsync(
        string refreshToken,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var tokenHash = ComputeHash(refreshToken);

        var database = redis.GetDatabase();

        await database.KeyDeleteAsync(
            GetKey(tokenHash));
    }

    private static string GenerateToken()
    {
        Span<byte> bytes = stackalloc byte[32];

        RandomNumberGenerator.Fill(bytes);

        return Convert.ToBase64String(bytes)
            .Replace("+", "-")
            .Replace("/", "_")
            .TrimEnd('=');
    }

    private static string ComputeHash(
        string refreshToken)
    {
        var bytes = Encoding.UTF8.GetBytes(refreshToken);

        var hash = SHA256.HashData(bytes);

        return Convert.ToHexString(hash);
    }

    private static RedisKey GetKey(
        string tokenHash)
    {
        return $"{KeyPrefix}{tokenHash}";
    }
}