using FluentValidation;

namespace Crm.Application.Features.Locations.CreateLocation;

public sealed class CreateLocationCommandValidator : AbstractValidator<CreateLocationCommand>
{
    public CreateLocationCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(150);

        RuleFor(x => x.ParentLocationId).GreaterThan(0).When(x => x.ParentLocationId.HasValue);
    }
}