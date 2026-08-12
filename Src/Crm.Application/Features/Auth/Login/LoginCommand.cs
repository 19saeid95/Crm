using MediatR;

namespace Crm.Application.Features.Auth.Login;

public sealed record LoginCommand(
    string UserName,
    string Password) : IRequest<LoginResponse>;