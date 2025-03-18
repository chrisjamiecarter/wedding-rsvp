namespace WeddingRsvp.Contracts.Requests.V1.Invites;

public sealed record UpdateInviteRequest(string Email,
                                         string HouseholdName);
