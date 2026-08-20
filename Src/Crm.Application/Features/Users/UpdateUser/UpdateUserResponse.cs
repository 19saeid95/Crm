namespace Crm.Application.Features.Users.UpdateUser;

public sealed record UpdateUserResponse(
    long Id,
    string UserName,
    string Phone,
    string? Name,
    string? LastName,
    long? ParentUserId,
    bool IsActive,
    bool IsSuperAdmin);