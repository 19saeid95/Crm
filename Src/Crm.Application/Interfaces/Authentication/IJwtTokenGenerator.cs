using Crm.Domain.Entities;

namespace Crm.Application.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}