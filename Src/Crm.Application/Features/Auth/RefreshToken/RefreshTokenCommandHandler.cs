using Crm.Application.Features.Auth.Login;
using Crm.Application.Interfaces.Authentication;
using Crm.Domain.Repositories;
using MediatR;

namespace Crm.Application.Features.Auth.RefreshToken;

public sealed class RefreshTokenCommandHandler(
    IRefreshTokenService refreshTokenService,
    IUserRepository userRepository,
    IJwtTokenGenerator jwtTokenGenerator)
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
        await refreshTokenService.RevokeAsync(
            request.RefreshToken,
            cancellationToken);

        var accessToken =
            jwtTokenGenerator.GenerateToken(user);

        var newRefreshToken =
            await refreshTokenService.CreateAsync(
                user.Id,
                cancellationToken);

        return new LoginResponse(
            accessToken,
            DateTime.UtcNow.AddHours(1),
            newRefreshToken);
    }
}