using WeddingRsvp.Application.Enums;
using WeddingRsvp.Application.Models;
using WeddingRsvp.Contracts.Requests.V1.Rsvps;

namespace WeddingRsvp.Api.Mappings.V1;

public static class SubmitRsvpMapping
{
    public static GuestRsvp ToModel(this GuestRsvpRequest request)
    {
        Enum.TryParse<RsvpStatus>(request.RsvpStatus, true, out var rsvpStatus);

        return new GuestRsvp
        {
            Id = request.GuestId,
            RsvpStatus = rsvpStatus,
            MainFoodOptionId = request.MainFoodOptionId,
            DessertFoodOptionId = request.DessertFoodOptionId,
        };
    }

    public static IEnumerable<GuestRsvp> ToModel(this IEnumerable<GuestRsvpRequest> requests)
    {
        return requests.Select(ToModel);
    }

    public static InviteRsvp ToModel(this SubmitRsvpRequest request, Guid id)
    {
        return new InviteRsvp
        {
            Id = id,
            Token = request.Token,
            Guests = request.Guests.ToModel(),
        };
    }
}
