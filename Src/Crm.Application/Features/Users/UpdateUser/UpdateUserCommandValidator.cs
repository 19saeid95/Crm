using FluentValidation;

namespace Crm.Application.Features.Users.UpdateUser;

public sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);

        RuleFor(x => x.UserName).NotEmpty().MaximumLength(50);

        RuleFor(x => x.Phone).NotEmpty().MaximumLength(15);

        RuleFor(x => x.Name).MaximumLength(50);

        RuleFor(x => x.LastName).MaximumLength(150);

        RuleFor(x => x.ParentUserId).GreaterThan(0).When(x => x.ParentUserId.HasValue);
    }
}