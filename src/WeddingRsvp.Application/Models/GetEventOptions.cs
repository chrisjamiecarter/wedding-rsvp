namespace WeddingRsvp.Application.Models;

public sealed class GetEventOptions
{
    public required Guid EventId { get; set; }
    public required string UserId { get; set; }
}
