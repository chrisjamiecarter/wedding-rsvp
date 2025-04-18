namespace WeddingRsvp.Contracts.Responses.V1.Guests;

public sealed record GuestDetailResponse(Guid Id,
                                         string Name,
                                         string RsvpStatus,
                                         Guid InviteId,
                                         string InviteHouseholdName,
                                         Guid? MainFoodOptionId,
                                         string? MainFoodOptionName,
                                         Guid? DessertFoodOptionId,
                                         string? DessertFoodOptionName);
