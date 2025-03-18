namespace WeddingRsvp.Contracts.Requests.V1.Invites;

public sealed record SubmitRsvpRequest(Guid Token, IEnumerable<GuestRsvpRequest> Guests);
