namespace Crm.Application.Contracts.Authentication;

public interface IOtpService
{
    Task<OtpData> GenerateAndStoreAsync(string key, CancellationToken cancellationToken = default);

    Task<bool> VerifyAsync(string key, string code, CancellationToken cancellationToken = default);
}

public sealed record OtpData(string Code, DateTime ExpiresAtUtc);