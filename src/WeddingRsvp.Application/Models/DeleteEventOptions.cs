namespace WeddingRsvp.Application.Models;

public sealed class DeleteEventOptions
{
    public required Guid EventId { get; set; }
    public required string UserId { get; set; }
}
