using WeddingRsvp.Application.Entities;
using WeddingRsvp.Application.Enums;
using WeddingRsvp.Application.Models;
using WeddingRsvp.Contracts.Requests;
using WeddingRsvp.Contracts.Requests.V1.Guests;
using WeddingRsvp.Contracts.Responses.V1.Guests;

namespace WeddingRsvp.Api.Mappings.V1;

public static class GuestMapping
{
    public static Guest ToEntity(this CreateGuestRequest request, Guid inviteId)
    {
        return new Guest
        {
            Id = Guid.CreateVersion7(),
            Name = request.Name,
            InviteId = inviteId,
            RsvpStatus = RsvpStatus.AwaitingResponse,
        };
    }

    public static Guest ToEntity(this UpdateGuestRequest request, Guest existingEntity)
    {
        Enum.TryParse<RsvpStatus>(request.RsvpStatus, true, out var rsvpStatus);

        return new Guest
        {
            Id = existingEntity.Id,
            Name = request.Name,
            RsvpStatus = rsvpStatus,
            InviteId = existingEntity.InviteId,
            MainFoodOptionId = request.MainFoodOptionId,
            DessertFoodOptionId = request.DessertFoodOptionId,
        };
    }

    public static GuestResponse ToResponse(this Guest entity)
    {
        return new GuestResponse(entity.Id,
                                 entity.Name,
                                 entity.RsvpStatus.ToString(),
                                 entity.InviteId,
                                 entity.MainFoodOptionId,
                                 entity.DessertFoodOptionId);
    }

    public static GuestDetailResponse ToDetailResponse(this Guest entity)
    {
        return new GuestDetailResponse(entity.Id,
                                       entity.Name,
                                       entity.RsvpStatus.ToString(),
                                       entity.InviteId,
                                       entity.Invite!.HouseholdName,
                                       entity.MainFoodOptionId,
                                       entity.MainFoodOption?.Name,
                                       entity.DessertFoodOptionId,
                                       entity.DessertFoodOption?.Name);
    }

    public static GuestDetailsResponse ToDetailsResponse(this PaginatedList<Guest> entities)
    {
        return new GuestDetailsResponse
        {
            Items = entities.Items.Select(ToDetailResponse),
            PageNumber = entities.PageNumber,
            PageSize = entities.PageSize,
            TotalCount = entities.TotalCount,
            TotalPages = entities.TotalPages,
        };
    }

    public static GetAllGuestsForEventOptions ToOptions(this GetAllGuestsForEventRequest request, Guid eventId)
    {
        Enum.TryParse<RsvpStatus>(request.RsvpStatus, true, out var rsvpStatus);

        return new GetAllGuestsForEventOptions
        {
            EventId = eventId,
            Name = request.Name,
            RsvpStatus = request.RsvpStatus is null
                ? null
                : rsvpStatus,
            HouseholdName = request.HouseholdName,
            SortField = request.SortBy?.TrimStart('-'),
            SortOrder = request.SortBy is null
                ? SortOrder.Unsorted
                : request.SortBy.StartsWith('-')
                    ? SortOrder.Descending
                    : SortOrder.Ascending,
            PageNumber = request.PageNumber.GetValueOrDefault(PagedRequest.DefaultPageNumber),
            PageSize = request.PageSize.GetValueOrDefault(PagedRequest.DefaultPageSize),
        };
    }

    public static GetAllGuestsForInviteOptions ToOptions(this GetAllGuestsForInviteRequest request, Guid inviteId)
    {
        Enum.TryParse<RsvpStatus>(request.RsvpStatus, true, out var rsvpStatus);

        return new GetAllGuestsForInviteOptions
        {
            InviteId = inviteId,
            Name = request.Name,
            RsvpStatus = request.RsvpStatus is null
                ? null
                : rsvpStatus,
            SortField = request.SortBy?.TrimStart('-'),
            SortOrder = request.SortBy is null
                ? SortOrder.Unsorted
                : request.SortBy.StartsWith('-')
                    ? SortOrder.Descending
                    : SortOrder.Ascending,
            PageNumber = request.PageNumber.GetValueOrDefault(PagedRequest.DefaultPageNumber),
            PageSize = request.PageSize.GetValueOrDefault(PagedRequest.DefaultPageSize),
        };
    }
}
