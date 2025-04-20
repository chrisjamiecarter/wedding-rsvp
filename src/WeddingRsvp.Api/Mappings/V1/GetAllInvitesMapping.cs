using WeddingRsvp.Application.Enums;
using WeddingRsvp.Application.Models;
using WeddingRsvp.Contracts.Requests;
using WeddingRsvp.Contracts.Requests.V1.Invites;

namespace WeddingRsvp.Api.Mappings.V1;

public static class GetAllInvitesMapping
{
    public static GetAllInvitesOptions ToOptions(this GetAllInvitesRequest request, Guid eventId)
    {
        return new GetAllInvitesOptions
        {
            EventId = eventId,
            Email = request.Email,
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
}
