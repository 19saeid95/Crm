namespace Crm.Application.Contracts.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(long userId, string userName);
}
