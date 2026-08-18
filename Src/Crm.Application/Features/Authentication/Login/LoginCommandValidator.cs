using FluentValidation;

namespace Crm.Application.Features.Authentication.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().MaximumLength(100).MinimumLength(100);
        RuleFor(x => x.Password).NotEmpty();
    }
}
