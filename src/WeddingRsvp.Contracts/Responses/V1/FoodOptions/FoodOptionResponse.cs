namespace WeddingRsvp.Contracts.Responses.V1.FoodOptions;

public sealed record FoodOptionResponse(Guid Id,
                                        Guid EventId,
                                        string Name,
                                        string FoodType);
