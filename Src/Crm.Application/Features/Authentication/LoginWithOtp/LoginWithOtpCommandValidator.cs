using FluentValidation;

namespace Crm.Application.Features.Authentication.LoginWithOtp;

public sealed class LoginWithOtpCommandValidator : AbstractValidator<LoginWithOtpCommand>
{
    public LoginWithOtpCommandValidator()
    {
        RuleFor(x => x.Phone).NotEmpty().MaximumLength(15);

        RuleFor(x => x.Code).NotEmpty().Length(4);
    }
}