using Crm.Application.Features.Auth.Login;
using MediatR;

namespace Crm.Application.Features.Auth.RefreshToken;

public sealed record RefreshTokenCommand(
    string RefreshToken) : IRequest<LoginResponse>;