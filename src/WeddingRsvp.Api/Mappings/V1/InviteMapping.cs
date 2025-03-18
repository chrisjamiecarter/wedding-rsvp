using WeddingRsvp.Application.Entities;
using WeddingRsvp.Contracts.Requests.V1.Invites;
using WeddingRsvp.Contracts.Responses.V1.Invites;

namespace WeddingRsvp.Api.Mappings.V1;

public static class InviteMapping
{
    public static Invite ToEntity(this CreateInviteRequest request, Guid eventId)
    {
        return new Invite
        {
            Id = Guid.CreateVersion7(),
            Email = request.Email,
            HouseholdName = request.HouseholdName,
            UniqueLinkToken = Guid.NewGuid(),
            EventId = eventId,
        };
    }

    public static Invite ToEntity(this UpdateInviteRequest request, Invite existingEntity)
    {
        return new Invite
        {
            Id = existingEntity.Id,
            Email = request.Email,
            HouseholdName = request.HouseholdName,
            UniqueLinkToken = existingEntity.UniqueLinkToken,
            EventId = existingEntity.EventId,
        };
    }

    public static InviteResponse ToResponse(this Invite entity)
    {
        return new InviteResponse(entity.Id,
                                  entity.Email,
                                  entity.HouseholdName,
                                  entity.UniqueLinkToken,
                                  entity.EventId);
    }

    public static InvitesResponse ToResponse(this IEnumerable<Invite> entities)
    {
        return new InvitesResponse(entities.Select(ToResponse));
    }
}
