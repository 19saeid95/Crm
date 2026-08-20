using Crm.Application.Common.Exceptions;
using Crm.Application.Contracts.Authentication;
using Crm.Domain.Repositories;
using MediatR;

namespace Crm.Application.Features.Authentication.LoginWithOtp;

public sealed class LoginWithOtpCommandHandler(IUserRepository userRepository, IOtpService otpService,
    IJwtTokenGenerator jwtTokenGenerator, IRefreshTokenGenerator refreshTokenGenerator, IRefreshTokenStore refreshTokenStore,IUnitOfWork unitOfWork)
    : IRequestHandler<LoginWithOtpCommand, LoginWithOtpResponse>
{
    public async Task<LoginWithOtpResponse> Handle(LoginWithOtpCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByPhoneAsync(request.Phone, cancellationToken);

        if (user is null)
            throw new UnauthorizedException("کد تأیید نامعتبر است.");

        if (!user.IsActive)
            throw new UnauthorizedException("حساب کاربری غیرفعال است.");

        var otpValid = await otpService.VerifyAsync(request.Phone, request.Code, cancellationToken);

        if (!otpValid)
            throw new UnauthorizedException("کد تأیید نامعتبر یا منقضی شده است.");

        user.LastLoginDate = DateTime.UtcNow;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        var accessToken = jwtTokenGenerator.GenerateToken(user.Id, user.UserName);

        var refreshToken = refreshTokenGenerator.Generate();

        await refreshTokenStore.StoreAsync(refreshToken.Token, user.Id, refreshToken.ExpiresAtUtc, cancellationToken);

        return new LoginWithOtpResponse(
            user.Id,
            user.UserName,
            accessToken,
            refreshToken.Token);
    }
}