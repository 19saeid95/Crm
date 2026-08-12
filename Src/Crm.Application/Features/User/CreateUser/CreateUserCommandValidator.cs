using FluentValidation;

namespace Crm.Application.Features.User.CreateUser;

public sealed class CreateUserCommandValidator
    : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8);

        RuleFor(x => x.Phone)
            .NotEmpty()
            .MaximumLength(15);

        RuleFor(x => x.Name)
            .MaximumLength(50);

        RuleFor(x => x.LastName)
            .MaximumLength(150);
    }
}