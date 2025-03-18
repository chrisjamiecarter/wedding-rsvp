namespace WeddingRsvp.Contracts.Responses.V1.Invites;

public sealed record SubmitRsvpResponse(Guid InviteId,
                                        string Email,
                                        string HouseholdName,
                                        Guid EventId,
                                        IEnumerable<GuestRsvpResponse> Guests);
