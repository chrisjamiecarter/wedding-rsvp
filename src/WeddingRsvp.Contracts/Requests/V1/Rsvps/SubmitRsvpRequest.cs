namespace WeddingRsvp.Contracts.Requests.V1.Rsvps;

public sealed record SubmitRsvpRequest(Guid Token, IEnumerable<GuestRsvpRequest> Guests);
