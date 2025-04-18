namespace WeddingRsvp.Contracts.Requests.V1.Rsvps;

public sealed record GuestRsvpRequest(Guid GuestId,
                                      string RsvpStatus,
                                      Guid? MainFoodOptionId,
                                      Guid? DessertFoodOptionId);
