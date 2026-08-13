using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Crm.Application.Interfaces.Authentication;
using StackExchange.Redis;

namespace Crm.Infrastructure.Authentication;

public sealed class RedisRefreshTokenService(
    IConnectionMultiplexer redis)
    : IRefreshTokenService
{
    private const string KeyPrefix = "crm:refresh-token:";
    private const string FamilyPrefix = "crm:refresh-family:";

    private static readonly TimeSpan RefreshTokenLifetime =
        TimeSpan.FromDays(7);

    public async Task<RefreshTokenResult> CreateAsync(
        long userId,
        string? familyId = null,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var token = GenerateToken();

        var tokenHash = ComputeHash(token);

        familyId ??= Guid.NewGuid().ToString("N");

        var data = new RefreshTokenData(
            userId,
            familyId,
            false);

        var database = redis.GetDatabase();

        await database.StringSetAsync(
            GetTokenKey(tokenHash),
            JsonSerializer.Serialize(data),
            RefreshTokenLifetime);

        await database.SetAddAsync(
            GetFamilyKey(familyId),
            tokenHash);

        await database.KeyExpireAsync(
            GetFamilyKey(familyId),
            RefreshTokenLifetime);

        return new RefreshTokenResult(
            token,
            familyId);
    }

    public async Task<RefreshTokenValidationResult?> ValidateAsync(
        string refreshToken,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var tokenHash = ComputeHash(refreshToken);

        var database = redis.GetDatabase();

        var value = await database.StringGetAsync(
            GetTokenKey(tokenHash));

        if (!value.HasValue)
            return null;

        var data = JsonSerializer.Deserialize<RefreshTokenData>(
            value.ToString());

        if (data is null)
            return null;

        return new RefreshTokenValidationResult(
            data.UserId,
            data.FamilyId,
            data.IsRevoked);
    }

    public async Task RevokeAsync(
        string refreshToken,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var tokenHash = ComputeHash(refreshToken);

        var database = redis.GetDatabase();

        var value = await database.StringGetAsync(
            GetTokenKey(tokenHash));

        if (!value.HasValue)
            return;

        var data = JsonSerializer.Deserialize<RefreshTokenData>(
            value.ToString());

        if (data is null)
            return;

        var revokedData = data with
        {
            IsRevoked = true
        };

        await database.StringSetAsync(
            GetTokenKey(tokenHash),
            JsonSerializer.Serialize(revokedData),
            RefreshTokenLifetime);
    }

    public async Task RevokeFamilyAsync(
        string familyId,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var database = redis.GetDatabase();

        var tokenHashes = await database.SetMembersAsync(
            GetFamilyKey(familyId));

        foreach (var tokenHash in tokenHashes)
        {
            var key = GetTokenKey(tokenHash!);

            var value = await database.StringGetAsync(key);

            if (!value.HasValue)
                continue;

            var data = JsonSerializer.Deserialize<RefreshTokenData>(
                value.ToString());

            if (data is null)
                continue;

            var revokedData = data with
            {
                IsRevoked = true
            };

            await database.StringSetAsync(
                key,
                JsonSerializer.Serialize(revokedData),
                RefreshTokenLifetime);
        }
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

    private static RedisKey GetTokenKey(
        string tokenHash)
    {
        return $"{KeyPrefix}{tokenHash}";
    }

    private static RedisKey GetFamilyKey(
        string familyId)
    {
        return $"{FamilyPrefix}{familyId}";
    }

    private sealed record RefreshTokenData(
        long UserId,
        string FamilyId,
        bool IsRevoked);
}