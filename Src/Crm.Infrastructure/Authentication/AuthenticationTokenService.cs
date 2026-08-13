using Crm.Application.Interfaces.Authentication;
using Crm.Domain.Entities;

public sealed class AuthenticationTokenService(
    IJwtTokenGenerator jwtTokenGenerator,
    IRefreshTokenService refreshTokenService)
    : IAuthenticationTokenService
{
    public async Task<AuthenticationTokenResult> GenerateAsync(
        User user,
        string? refreshTokenFamilyId = null,
        CancellationToken cancellationToken = default)
    {
        var jwtToken =
            jwtTokenGenerator.GenerateToken(user);

        var refreshToken =
            await refreshTokenService.CreateAsync(
                user.Id,
                refreshTokenFamilyId,
                cancellationToken);

        return new AuthenticationTokenResult(
            jwtToken.AccessToken,
            jwtToken.ExpiresAt,
            refreshToken.Token);
    }
}