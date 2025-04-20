using WeddingRsvp.Application.Entities;
using WeddingRsvp.Application.Models;
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
            Token = Guid.NewGuid(),
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
            Token = existingEntity.Token,
            EventId = existingEntity.EventId,
        };
    }

    public static InviteResponse ToResponse(this Invite entity)
    {
        return new InviteResponse(entity.Id,
                                  entity.Email,
                                  entity.HouseholdName,
                                  entity.Token,
                                  entity.EventId);
    }

    public static InvitesResponse ToResponse(this PaginatedList<Invite> entities)
    {
        return new InvitesResponse
        {
            Items = entities.Items.Select(ToResponse),
            PageNumber = entities.PageNumber,
            PageSize = entities.PageSize,
            TotalCount = entities.TotalCount,
            TotalPages = entities.TotalPages,
        };
    }
}
