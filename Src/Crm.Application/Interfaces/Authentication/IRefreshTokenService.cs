namespace Crm.Application.Interfaces.Authentication;

public interface IRefreshTokenService
{
    Task<string> CreateAsync(
        long userId,
        CancellationToken cancellationToken = default);

    Task<long?> ValidateAsync(
        string refreshToken,
        CancellationToken cancellationToken = default);

    Task RevokeAsync(
        string refreshToken,
        CancellationToken cancellationToken = default);
}