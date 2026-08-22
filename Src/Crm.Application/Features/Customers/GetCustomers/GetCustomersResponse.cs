namespace Crm.Application.Features.Customers.GetCustomers;

public sealed class GetCustomersResponse
{
    public long Id { get; init; }
    public long UserId { get; init; }
    public string UserName { get; init; } = null!;
    public string Phone { get; init; } = null!;
    public string? Name { get; init; }
    public string? LastName { get; init; }
    public long LocationId { get; init; }
    public string LocationName { get; init; } = null!;
    public string CustomerCode { get; init; } = null!;
    public bool IsActive { get; init; }
    public int? PurchasePerformanceScore { get; init; }
    public int? PurchaseMixQualityScore { get; init; }
    public int? StoreCapacityScore { get; init; }
    public int? LoyaltyStrategicCooperationScore { get; init; }
    public int? ProfessionalStaffQualityScore { get; init; }
    public int? RegionalMarketPotentialScore { get; init; }
}