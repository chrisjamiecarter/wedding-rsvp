using WeddingRsvp.Application.Entities;

namespace WeddingRsvp.Application.Models;

public sealed class UpdateEventOptions
{
    public required Event Event { get; set; }
    public required string UserId { get; set; }
}
