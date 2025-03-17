namespace WeddingRsvp.Contracts.Responses.V1.EventFoodOptions;

public sealed record EventFoodOptionResponse(Guid Id,
                                             Guid EventId,
                                             Guid FoodOptionId,
                                             string FoodOptionName,
                                             string FoodOptionFoodType);
