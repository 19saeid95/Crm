public sealed record CreateCustomerResponse(
    long Id,
    long UserId,
    string CustomerName,
    string CustomerCode,
    string Phone,
    long LocationId,
    bool IsActive,
    int? PurchasePerformanceScore,
    int? PurchaseMixQualityScore,
    int? StoreCapacityScore,
    int? LoyaltyStrategicCooperationScore,
    int? ProfessionalStaffQualityScore,
    int? RegionalMarketPotentialScore);