namespace WeddingRsvp.Contracts.Requests.V1.Invites;

public sealed record CreateInviteRequest(string Email,
                                         string HouseholdName);
