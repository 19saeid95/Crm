using Crm.Application.Interfaces.Authentication;
using Crm.Domain.Entities;

public interface IAuthenticationTokenService
{
    Task<AuthenticationTokenResult> GenerateAsync(
        User user,
        string? refreshTokenFamilyId = null,
        CancellationToken cancellationToken = default);
}