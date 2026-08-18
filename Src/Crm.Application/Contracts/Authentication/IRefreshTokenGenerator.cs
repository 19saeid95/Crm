namespace Crm.Application.Contracts.Authentication;

public interface IRefreshTokenGenerator
{
    RefreshTokenData Generate();
}

public sealed record RefreshTokenData(string Token, DateTime ExpiresAtUtc);