namespace Crm.Application.Features.Customers.CreateCustomer;

public sealed record CreateCustomerResponse(
    long Id,
    long UserId,
    long LocationId,
    string CustomerCode,
    bool IsActive,
    int? PurchasePerformanceScore,
    int? PurchaseMixQualityScore,
    int? StoreCapacityScore,
    int? LoyaltyStrategicCooperationScore,
    int? ProfessionalStaffQualityScore,
    int? RegionalMarketPotentialScore);