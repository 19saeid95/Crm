using Crm.Domain.Entities;

namespace Crm.Application.Interfaces.Authentication;

public interface IAuthenticationTokenService
{
    Task<AuthenticationTokenResult> GenerateAsync(
        User user,
        CancellationToken cancellationToken = default);
}