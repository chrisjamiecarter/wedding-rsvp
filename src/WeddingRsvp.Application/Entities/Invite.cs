namespace WeddingRsvp.Application.Entities;

public sealed class Invite
{
    public required Guid Id { get; set; }
    public required string Email { get; set; }
    public required string HouseholdName { get; set; }
    public Guid? UniqueLinkToken { get; set; }

    public Guid EventId { get; set; }
    public Event? Event { get; set; }

    public ICollection<Guest>? Guests { get; }
}
