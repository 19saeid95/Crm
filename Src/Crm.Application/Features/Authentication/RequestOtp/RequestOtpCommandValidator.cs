using FluentValidation;

namespace Crm.Application.Features.Authentication.RequestOtp;

public sealed class RequestOtpCommandValidator: AbstractValidator<RequestOtpCommand>
{
    public RequestOtpCommandValidator()
    {
        RuleFor(x => x.Phone).NotEmpty().MaximumLength(15);
    }
}