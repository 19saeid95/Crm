using MediatR;

namespace Crm.Application.Features.User.CreateUser;

public sealed record CreateUserCommand(
    string? Name,
    string? LastName,
    string UserName,
    string Password,
    string Phone) : IRequest<CreateUserResponse>;