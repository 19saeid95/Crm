using Crm.Application.Common.Exceptions;
using Crm.Domain.Repositories;
using Crm.Domain.Services;
using MediatR;

namespace Crm.Application.Features.Authentication.Login;

public class LoginCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    : IRequestHandler<LoginCommand, LoginResponse>
{
    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByUserNameAsync(request.UserName, cancellationToken);

        if (user is null)
            throw new UnauthorizedException("نام کاربری یا رمز عبور اشتباه است.");

        if (!user.IsActive)
            throw new UnauthorizedException("حساب کاربری غیرفعال است.");

        var passwordValid = passwordHasher.Verify(request.Password, user.PasswordHash);
        if (!passwordValid)
            throw new UnauthorizedException("نام کاربری یا رمز عبور اشتباه است.");

        return new LoginResponse(user.Id, user.UserName);
    }
}
