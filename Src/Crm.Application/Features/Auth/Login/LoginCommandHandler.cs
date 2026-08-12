using Crm.Application.Interfaces.Authentication;
using Crm.Domain.Repositories;
using MediatR;

namespace Crm.Application.Features.Auth.Login;

public sealed class LoginCommandHandler(
    IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator) : IRequestHandler<LoginCommand, LoginResponse>
{
    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByUserNameAsync(
            request.UserName,
            cancellationToken);

        if (user is null)
            throw new UnauthorizedAccessException(
                "نام کاربری یا رمز عبور اشتباه است.");

        var passwordValid = passwordHasher.Verify(
            request.Password,
            user.PasswordHash);

        if (!passwordValid)
            throw new UnauthorizedAccessException(
                "نام کاربری یا رمز عبور اشتباه است.");

        var accessToken = jwtTokenGenerator.GenerateToken(user);

        return new LoginResponse(
            accessToken,
            DateTime.UtcNow.AddHours(1));
    }
}