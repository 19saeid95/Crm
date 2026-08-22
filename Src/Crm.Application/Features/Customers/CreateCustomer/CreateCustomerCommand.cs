using Crm.Application.Features.Customers.CreateCustomer;
using MediatR;

public sealed record CreateCustomerCommand(
    long UserId,
    long LocationId,
    string CustomerCode,
    int PurchasePerformanceScore,
    int PurchaseMixQualityScore,
    int StoreCapacityScore,
    int LoyaltyStrategicCooperationScore,
    int ProfessionalStaffQualityScore,
    int RegionalMarketPotentialScore
) : IRequest<CreateCustomerResponse>;