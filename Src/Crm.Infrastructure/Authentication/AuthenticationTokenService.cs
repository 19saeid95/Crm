using Crm.Application.Interfaces.Authentication;
using Crm.Domain.Entities;

namespace Crm.Infrastructure.Authentication;

public sealed class AuthenticationTokenService(
    IJwtTokenGenerator jwtTokenGenerator,
    IRefreshTokenService refreshTokenService)
    : IAuthenticationTokenService
{
    public async Task<AuthenticationTokenResult> GenerateAsync(
        User user,
        CancellationToken cancellationToken = default)
    {
        var jwtToken =
            jwtTokenGenerator.GenerateToken(user);

        var refreshToken =
            await refreshTokenService.CreateAsync(
                user.Id,
                cancellationToken);

        return new AuthenticationTokenResult(
            jwtToken.AccessToken,
            jwtToken.ExpiresAt,
            refreshToken);
    }
}