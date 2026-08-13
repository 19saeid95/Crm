using Crm.Domain.Entities;

namespace Crm.Application.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    JwtTokenResult GenerateToken(User user);
}

public sealed record JwtTokenResult(
    string AccessToken,
    DateTime ExpiresAt);