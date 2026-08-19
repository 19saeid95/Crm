using Crm.Application.Common.Exceptions;
using Crm.Application.Contracts.Authentication;
using Crm.Domain.Repositories;
using MediatR;

namespace Crm.Application.Features.Authentication.RefreshToken;

public class RefreshTokenCommandHandler(IRefreshTokenStore refreshTokenStore, IUserRepository userRepository
    , IJwtTokenGenerator jwtTokenGenerator, IRefreshTokenGenerator refreshTokenGenerator)
    : IRequestHandler<RefreshTokenCommand, RefreshTokenResponse>
{
    public async Task<RefreshTokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var userId = await refreshTokenStore.GetUserIdAsync(request.RefreshToken, cancellationToken);

        if (userId is null)
            throw new UnauthorizedException("Refresh Token نامعتبر یا منقضی شده است.");

        var user = await userRepository.GetByUserIdAsync(userId.Value, cancellationToken);

        if (user is null || !user.IsActive)
            throw new UnauthorizedException("کاربر معتبر نیست.");

        var accessToken = jwtTokenGenerator.GenerateToken(user.Id, user.UserName);

        var newRefreshToken = refreshTokenGenerator.Generate();

        await refreshTokenStore.RemoveAsync(request.RefreshToken, cancellationToken);

        await refreshTokenStore.StoreAsync(
            newRefreshToken.Token,
            user.Id,
            newRefreshToken.ExpiresAtUtc,
            cancellationToken);

        return new RefreshTokenResponse(accessToken, newRefreshToken.Token);
    }
}

