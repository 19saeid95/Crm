using FluentValidation;

namespace Crm.Application.Features.Customers.CreateCustomer;

public sealed class CreateCustomerCommandValidator
    : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(x => x.CustomerName).NotEmpty();

        RuleFor(x => x.LocationId).GreaterThan(0);

        RuleFor(x => x.CustomerCode).NotEmpty().MaximumLength(50);

        RuleFor(x => x.PurchasePerformanceScore).GreaterThanOrEqualTo(0);

        RuleFor(x => x.PurchaseMixQualityScore).GreaterThanOrEqualTo(0);

        RuleFor(x => x.StoreCapacityScore).GreaterThanOrEqualTo(0);

        RuleFor(x => x.LoyaltyStrategicCooperationScore).GreaterThanOrEqualTo(0);

        RuleFor(x => x.ProfessionalStaffQualityScore).GreaterThanOrEqualTo(0);

        RuleFor(x => x.RegionalMarketPotentialScore).GreaterThanOrEqualTo(0);
    }
}