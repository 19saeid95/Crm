using MediatR;

namespace Crm.Application.Features.Authentication.Login;

public record LoginCommand(string UserName, string Password) : IRequest<LoginResponse>;
