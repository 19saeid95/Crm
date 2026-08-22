using Crm.Application.Features.Customers.CreateCustomer;
using MediatR;

public sealed record CreateCustomerCommand(
   string CustomerName,
    string CustomerCode,
    string Phone,
    long LocationId,
    int PurchasePerformanceScore,
    int PurchaseMixQualityScore,
    int StoreCapacityScore,
    int LoyaltyStrategicCooperationScore,
    int ProfessionalStaffQualityScore,
    int RegionalMarketPotentialScore
) : IRequest<CreateCustomerResponse>;