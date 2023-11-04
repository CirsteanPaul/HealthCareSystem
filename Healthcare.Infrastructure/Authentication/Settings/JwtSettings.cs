namespace Healthcare.Infrastructure.Authentication.Settings;

public class JwtSettings
{
    public const string SettingsKey = "Jwt";
    
    public required string Key { get; init; }
    public required string Issuer { get; init; }
    public required string Audience { get; init; }
    public required int TokenLifetimeSeconds { get; init; }
}