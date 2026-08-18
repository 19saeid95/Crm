using System.Security.Cryptography;
using Crm.Application.Contracts.Authentication;
using Microsoft.Extensions.Options;

namespace Crm.Infrastructure.Authentication;

public sealed class RefreshTokenGenerator(IOptions<JwtOptions> options)
    : IRefreshTokenGenerator
{
    public RefreshTokenData Generate()
    {
        var bytes = RandomNumberGenerator.GetBytes(64);

        var token = Convert.ToBase64String(bytes).Replace('+', '-').Replace('/', '_').TrimEnd('=');

        var expiresAtUtc =DateTime.UtcNow.AddDays(options.Value.RefreshTokenExpirationDays);

        return new RefreshTokenData(token,expiresAtUtc);
    }
}