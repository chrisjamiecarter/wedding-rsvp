namespace WeddingRsvp.Application.Options;

public class JwtOptions
{
    public required string Audience { get; set; }
    public required string Issuer { get; set; }
    public required string Secret { get; set; }
    public required int AccessTokenLifetimeInMinutes { get; set; }
}
