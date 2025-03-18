namespace WeddingRsvp.Contracts.Responses.V1.Invites;

public sealed record InviteResponse(Guid Id,
                                    string Email,
                                    string HouseholdName,
                                    Guid? UniqueLinkToken,
                                    Guid EventId);
