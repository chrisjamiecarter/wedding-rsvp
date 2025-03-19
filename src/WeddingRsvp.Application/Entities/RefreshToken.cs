using System.Security.Cryptography;

namespace WeddingRsvp.Application.Entities;

public sealed class RefreshToken
{
    public required Guid Id { get; set; }
    public required string Value { get; set; }
    public DateTime ExpiresOnUtc { get; set; }

    public required string UserId { get; set; }
    public ApplicationUser? User { get; set; }

    public static RefreshToken Create(string userId)
    {
        return new RefreshToken
        {
            Id = Guid.CreateVersion7(),
            UserId = userId,
            Value = GenerateValue(),
            ExpiresOnUtc = GenerateExpiresOnUtc(),
        };
    }

    public void Refresh()
    {
        Value = GenerateValue();
        ExpiresOnUtc = GenerateExpiresOnUtc();
    }

    private static string GenerateValue()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
    }

    private static DateTime GenerateExpiresOnUtc()
    {
        return DateTime.UtcNow.AddDays(7);
    }
}
