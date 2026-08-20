namespace Crm.Application.Features.Authentication.LoginWithOtp;

public sealed record LoginWithOtpResponse(long UserId, string UserName, string AccessToken, string RefreshToken);