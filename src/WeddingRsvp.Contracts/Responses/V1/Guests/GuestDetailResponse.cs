namespace WeddingRsvp.Contracts.Responses.V1.Guests;

public sealed record GuestDetailResponse(Guid Id,
                                         string Name,
                                         string RvspStatus,
                                         Guid InviteId,
                                         string InviteHouseholdName,
                                         Guid? MainFoodOptionId,
                                         string? MainFoodOptionName,
                                         Guid? DessertFoodOptionId,
                                         string? DessertFoodOptionName);
