namespace WeddingRsvp.Contracts.Responses.V1.Invites;

public sealed record GuestRsvpResponse(Guid GuestId,
                                       string Name,
                                       string RsvpStatus,
                                       Guid? MainFoodOptionId,
                                       string? MainFoodOptionName,
                                       Guid? DessertFoodOptionId,
                                       string? DessertFoodOptionName);
