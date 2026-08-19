namespace Crm.Application.Features.Authentication.RefreshToken;

public sealed record RefreshTokenResponse(string AccessToken, string RefreshToken);