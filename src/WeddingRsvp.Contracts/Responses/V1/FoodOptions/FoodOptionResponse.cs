namespace WeddingRsvp.Contracts.Responses.V1.FoodOptions;

public sealed record FoodOptionResponse(Guid Id,
                                        string Name,
                                        string FoodType);
