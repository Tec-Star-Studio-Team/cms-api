namespace CmsApi.Server.Infrastructure.Settings;

public sealed class JwtSettings
{
    public const string SectionName = "JwtSettings";

    public string Issuer { get; init; } = string.Empty;
    public string Audience { get; init; } = string.Empty;
    public string SecretKey { get; init; } = string.Empty;
    public int ExpirationInMinutes { get; init; } = 60;
}
