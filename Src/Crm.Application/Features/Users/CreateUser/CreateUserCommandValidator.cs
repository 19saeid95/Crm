using FluentValidation;

namespace Crm.Application.Features.Users.CreateUser;

public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().MaximumLength(50);

        RuleFor(x => x.Password).NotEmpty().MinimumLength(6);

        RuleFor(x => x.Phone).NotEmpty().MaximumLength(15);

        RuleFor(x => x.Name).MaximumLength(50);

        RuleFor(x => x.LastName).MaximumLength(150);

        RuleFor(x => x.ParentUserId).GreaterThan(0).When(x => x.ParentUserId.HasValue);
    }
}
