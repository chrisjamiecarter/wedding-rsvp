namespace WeddingRsvp.Contracts.Responses.V1.Guests;

public sealed record GuestResponse(Guid Id,
                                   string Name,
                                   string RsvpStatus,
                                   Guid InviteId,
                                   Guid? MainFoodOptionId,
                                   Guid? DessertFoodOptionId);
