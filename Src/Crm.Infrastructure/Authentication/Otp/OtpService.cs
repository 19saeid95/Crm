using Crm.Application.Contracts.Authentication;
using Crm.Application.Contracts.Communication;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Security.Cryptography;
using System.Text;

namespace Crm.Infrastructure.Authentication;

public sealed class OtpService(IConnectionMultiplexer redis, IOptions<OtpOptions> options) : IOtpService
{
    private const string KeyPrefix = "otp:";

    public async Task<OtpData> GenerateAndStoreAsync(string key, CancellationToken cancellationToken = default)
    {
        var code = GenerateCode(options.Value.CodeLength);
        var expiresAtUtc = DateTime.UtcNow.AddMinutes(options.Value.ExpirationMinutes);
        var hash = Hash(code);
        var redisKey = KeyPrefix + key;
        var database = redis.GetDatabase();
        var ttl = expiresAtUtc - DateTime.UtcNow;
        await database.StringSetAsync(redisKey, hash, ttl);

        //send sms
        try
        {
          //  await smsSender.SendAsync(key, $"کد ورود شما: {code}", cancellationToken);
        }
        catch
        {
            await database.KeyDeleteAsync(redisKey);throw;
        }
        return new OtpData(code, expiresAtUtc);
    }

    public async Task<bool> VerifyAsync(string key, string code, CancellationToken cancellationToken = default)
    {
        var redisKey = KeyPrefix + key;
        var database = redis.GetDatabase();
        var storedHash = await database.StringGetAsync(redisKey);
        if (!storedHash.HasValue)
            return false;

        var providedHash = Hash(code);

        if (!CryptographicOperations.FixedTimeEquals(
                Convert.FromHexString((string)storedHash!),
                Convert.FromHexString((string)providedHash)))
            return false;

        await database.KeyDeleteAsync(redisKey);

        return true;
    }

    private static string GenerateCode(int length)
    {
        var min = (int)Math.Pow(10, length - 1);
        var max = (int)Math.Pow(10, length);

        return RandomNumberGenerator.GetInt32(min, max).ToString();
    }

    private static string Hash(string value)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(value));
        return Convert.ToHexString(bytes);
    }
}