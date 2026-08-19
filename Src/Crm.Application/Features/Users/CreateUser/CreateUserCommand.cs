using MediatR;

namespace Crm.Application.Features.Users.CreateUser;

public sealed record CreateUserCommand(
     string UserName,
    string Password,
    string Phone,
    string? Name,
    string? LastName,
    long? ParentUserId
    ) : IRequest<CreateUserResponse>;
