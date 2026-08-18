namespace Crm.Application.Contracts.Authentication;

public interface IRefreshTokenStore
{
    Task StoreAsync(string refreshToken, long userId, DateTime expiresAtUtc, CancellationToken cancellationToken = default);

    Task<long?> GetUserIdAsync(string refreshToken, CancellationToken cancellationToken = default);

    Task RemoveAsync(string refreshToken, CancellationToken cancellationToken = default);
}