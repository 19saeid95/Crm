namespace Crm.Infrastructure.Authentication;

public sealed class OtpOptions
{
    public const string SectionName = "Otp";

    public int ExpirationMinutes { get; set; } = 2;

    public int CodeLength { get; set; } = 4;
}