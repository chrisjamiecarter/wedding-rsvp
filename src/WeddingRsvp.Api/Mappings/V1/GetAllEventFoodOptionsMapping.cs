using WeddingRsvp.Application.Enums;
using WeddingRsvp.Application.Models;
using WeddingRsvp.Contracts.Requests;
using WeddingRsvp.Contracts.Requests.V1.EventFoodOptions;

namespace WeddingRsvp.Api.Mappings.V1;

public static class GetAllEventFoodOptionsMapping
{
    public static GetAllEventFoodOptionsOptions ToOptions(this GetAllEventFoodOptionsRequest request, Guid eventId)
    {
        Enum.TryParse<FoodType>(request.FoodType, true, out var foodType);

        return new GetAllEventFoodOptionsOptions
        {
            EventId = eventId,
            FoodType = request.FoodType is null
                ? null
                : foodType,
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