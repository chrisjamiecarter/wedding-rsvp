namespace WeddingRsvp.Contracts.Requests.V1.Invites;

public sealed record GuestRsvpRequest(Guid GuestId,
                                      string RsvpStatus,
                                      Guid? MainFoodOptionId,
                                      Guid? DessertFoodOptionId);
