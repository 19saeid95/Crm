using Crm.Application.Features.Auth.Login;
using Crm.Application.Features.Auth.RefreshToken;
using Crm.Application.Interfaces.Authentication;
using MediatR;

public sealed class RefreshTokenCommandHandler(
    IRefreshTokenService refreshTokenService,
    IUserRepository userRepository,
    IAuthenticationTokenService authenticationTokenService)
    : IRequestHandler<RefreshTokenCommand, LoginResponse>
{
    public async Task<LoginResponse> Handle(
        RefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        var token = await refreshTokenService.ValidateAsync(
            request.RefreshToken,
            cancellationToken);

        if (token is null)
        {
            throw new UnauthorizedAccessException(
                "Refresh Token نامعتبر یا منقضی شده است.");
        }

        if (token.IsRevoked)
        {
            await refreshTokenService.RevokeFamilyAsync(
                token.FamilyId,
                cancellationToken);

            throw new UnauthorizedAccessException(
                "Refresh Token reuse detected.");
        }

        var user = await userRepository.GetByIdAsync(
            token.UserId,
            cancellationToken);

        if (user is null || user.IsDeleted)
        {
            throw new UnauthorizedAccessException(
                "کاربر معتبر نیست.");
        }

        await refreshTokenService.RevokeAsync(
            request.RefreshToken,
            cancellationToken);

        var tokens =
            await authenticationTokenService.GenerateAsync(
                user,
                token.FamilyId,
                cancellationToken);

        return new LoginResponse(
            tokens.AccessToken,
            tokens.ExpiresAt,
            tokens.RefreshToken);
    }
}