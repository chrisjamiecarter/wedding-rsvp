namespace WeddingRsvp.Application.Models;

public sealed class InviteRsvp
{
    public Guid Id { get; set; }
    public Guid Token { get; set; }
    public IEnumerable<GuestRsvp> Guests { get; set; } = [];
}