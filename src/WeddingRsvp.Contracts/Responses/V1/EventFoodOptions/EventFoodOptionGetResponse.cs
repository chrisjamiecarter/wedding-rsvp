namespace WeddingRsvp.Contracts.Responses.V1.EventFoodOptions;

public sealed record EventFoodOptionGetResponse(Guid Id,
                                                Guid EventId,
                                                Guid FoodOptionId);
