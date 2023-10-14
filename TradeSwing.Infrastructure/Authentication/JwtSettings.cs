namespace TradeSwing.Infrastructure.Authentication;

public class JwtSettings
{
    public const string SectionName = "JwtSettings";
    
    public string Secret { init; get; } = null!;
    public string Issuer { init; get; } = null!;
    public string Audience { init; get; } = null!;
    public int ExpiryInMinutes { init; get; }
}