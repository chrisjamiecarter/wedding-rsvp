using WeddingRsvp.Application.Enums;
using WeddingRsvp.Application.Models;
using WeddingRsvp.Contracts.Requests;
using WeddingRsvp.Contracts.Requests.V1.Events;

namespace WeddingRsvp.Api.Mappings.V1;

public static class GetAllEventsMapping
{
    public static GetAllEventsOptions ToOptions(this GetAllEventsRequest request, string userId)
    {
        return new GetAllEventsOptions
        {
            UserId = userId,
            Name = request.Name,
            Venue = request.Venue,
            DateFrom = request.DateFrom,
            DateTo = request.DateTo,
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
