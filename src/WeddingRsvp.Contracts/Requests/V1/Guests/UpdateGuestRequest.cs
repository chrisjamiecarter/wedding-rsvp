namespace WeddingRsvp.Contracts.Requests.V1.Guests;

public sealed record UpdateGuestRequest(string Name,
                                        string RvspStatus,
                                        Guid? MainFoodOptionId,
                                        Guid? DessertFoodOptionId);
