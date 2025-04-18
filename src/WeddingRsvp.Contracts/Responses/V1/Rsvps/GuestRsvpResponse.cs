namespace WeddingRsvp.Contracts.Responses.V1.Rsvps;

public sealed record GuestRsvpResponse(Guid GuestId,
                                       string Name);
