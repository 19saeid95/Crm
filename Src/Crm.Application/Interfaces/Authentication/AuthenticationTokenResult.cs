namespace Crm.Application.Interfaces.Authentication;

public sealed record AuthenticationTokenResult(
    string AccessToken,
    DateTime ExpiresAt,
    string RefreshToken);