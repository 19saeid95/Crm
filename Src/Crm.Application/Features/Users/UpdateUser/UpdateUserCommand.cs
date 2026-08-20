using MediatR;

namespace Crm.Application.Features.Users.UpdateUser;
public sealed record UpdateUserCommand(
    long Id,
    string UserName,
    string Phone,
    string? Name,
    string? LastName,
    long? ParentUserId
) : IRequest<UpdateUserResponse>;