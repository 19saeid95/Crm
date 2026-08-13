namespace Crm.Application.Interfaces.Authentication;

public interface IRefreshTokenService
{
    Task<RefreshTokenResult> CreateAsync(
        long userId,
        string? familyId = null,
        CancellationToken cancellationToken = default);

    Task<RefreshTokenValidationResult?> ValidateAsync(
        string refreshToken,
        CancellationToken cancellationToken = default);

    Task RevokeAsync(
        string refreshToken,
        CancellationToken cancellationToken = default);

    Task RevokeFamilyAsync(
        string familyId,
        CancellationToken cancellationToken = default);
}

public sealed record RefreshTokenResult(
    string Token,
    string FamilyId);

public sealed record RefreshTokenValidationResult(
    long UserId,
    string FamilyId,
    bool IsRevoked);