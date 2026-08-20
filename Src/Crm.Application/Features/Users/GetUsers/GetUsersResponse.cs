namespace Crm.Application.Features.Users.GetUsers;

public sealed record GetUsersResponse(
    long Id,
    string UserName,
    string Phone,
    string? Name,
    string? LastName,
    long? ParentUserId,
    bool IsActive,
    bool IsSuperAdmin);