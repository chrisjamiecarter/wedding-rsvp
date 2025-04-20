using WeddingRsvp.Application.Entities;
using WeddingRsvp.Contracts.Responses.V1.Rsvps;

namespace WeddingRsvp.Api.Mappings.V1;

public static class RsvpMapping
{
    public static GetRsvpResponse ToGetRsvpResponse(this Invite invite,
                                                    IEnumerable<Guest> guests,
                                                    IEnumerable<FoodOption> mains,
                                                    IEnumerable<FoodOption> desserts)
    {
        if (invite.Token is null)
        {
            throw new InvalidOperationException("Invite RSVP must have a non-null token value.");
        }
        return new GetRsvpResponse(invite.Token.Value,
                                   guests.ToGuestsRsvpResponse(),
                                   mains.ToFoodOptionsRsvpResponse(),
                                   desserts.ToFoodOptionsRsvpResponse());
    }

    public static IEnumerable<FoodOptionRsvpResponse> ToFoodOptionsRsvpResponse(this IEnumerable<FoodOption> foodOptions)
    {
        return foodOptions.Select(ToFoodOptionRsvpResponse);
    }

    public static IEnumerable<GuestRsvpResponse> ToGuestsRsvpResponse(this IEnumerable<Guest> guests)
    {
        return guests.Select(ToGuestRsvpResponse);
    }

    public static FoodOptionRsvpResponse ToFoodOptionRsvpResponse(this FoodOption foodOption)
    {
        return new FoodOptionRsvpResponse(foodOption.Id,
                                          foodOption.Name);
    }

    public static GuestRsvpResponse ToGuestRsvpResponse(this Guest guest)
    {
        return new GuestRsvpResponse(guest.Id,
                                     guest.Name);
    }
}
