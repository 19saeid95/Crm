using Crm.Application.Features.Auth.Login;
using Crm.Application.Interfaces.Authentication;
using Crm.Domain.Repositories;
using MediatR;

namespace Crm.Application.Features.Auth.RefreshToken;

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
        var userId =
            await refreshTokenService.ValidateAsync(
                request.RefreshToken,
                cancellationToken);

        if (userId is null)
        {
            throw new UnauthorizedAccessException(
                "Refresh Token نامعتبر یا منقضی شده است.");
        }

        var user = await userRepository.GetByIdAsync(
            userId.Value,
            cancellationToken);

        if (user is null || user.IsDeleted)
        {
            throw new UnauthorizedAccessException(
                "کاربر معتبر نیست.");
        }

        // Rotation:
        // Refresh Token قبلی باطل می‌شود.
        var tokens =
     await authenticationTokenService.GenerateAsync(
         user,
         cancellationToken);

        return new LoginResponse(
            tokens.AccessToken,
            tokens.ExpiresAt,
            tokens.RefreshToken);
    }
}