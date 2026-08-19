
namespace Crm.Application.Features.Users.CreateUser;

public sealed record CreateUserResponse(
    long Id,
    string UserName,
    string Phone,
    string? Name,
    string? LastName,
    long? ParentUserId,
    bool IsActive);
