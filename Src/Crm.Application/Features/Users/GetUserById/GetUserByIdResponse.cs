namespace Crm.Application.Features.Users.GetUserById;

public sealed record GetUserByIdResponse(
    long Id,
    string UserName,
    string Phone,
    string? Name,
    string? LastName,
    long? ParentUserId,
    bool IsActive,
    bool IsSuperAdmin,
    DateTime? LastLoginDate);
