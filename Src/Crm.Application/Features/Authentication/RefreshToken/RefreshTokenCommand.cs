using MediatR;

namespace Crm.Application.Features.Authentication.RefreshToken;

public sealed record RefreshTokenCommand(string RefreshToken) : IRequest<RefreshTokenResponse>;