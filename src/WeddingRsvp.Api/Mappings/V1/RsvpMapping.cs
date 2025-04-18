using WeddingRsvp.Application.Entities;
using WeddingRsvp.Contracts.Responses.V1.Rsvps;

namespace WeddingRsvp.Api.Mappings.V1;

public static class RsvpMapping
{
    public static GetRsvpResponse ToGetRsvpResponse(this Invite entity, IEnumerable<Guest> entities)
    {
        if (entity.Token is null)
        {
            throw new InvalidOperationException("Invite RSVP must have a non-null token value.");
        }
        return new GetRsvpResponse(entity.Token.Value,
                                   entities.ToGuestRsvpResponse());
    }

    public static IEnumerable<GuestRsvpResponse> ToGuestRsvpResponse(this IEnumerable<Guest> entities)
    {
        return entities.Select(ToGuestRsvpResponse);
    }

    public static GuestRsvpResponse ToGuestRsvpResponse(this Guest entity)
    {
        return new GuestRsvpResponse(entity.Id,
                                     entity.Name);
    }
}
