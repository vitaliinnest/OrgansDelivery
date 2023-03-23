namespace OrganStorage.BL.Models.Options;

public class JwtSettings
{
    public string Issuer { get; init; }
    public string Audience { get; init; }
    public string Secret { get; init; }
    public int ExpiresInMinutes { get; init; }
}
