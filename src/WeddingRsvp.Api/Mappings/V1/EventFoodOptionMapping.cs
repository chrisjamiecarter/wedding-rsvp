using WeddingRsvp.Application.Entities;
using WeddingRsvp.Application.Enums;
using WeddingRsvp.Application.Models;
using WeddingRsvp.Contracts.Requests.V1.EventFoodOptions;
using WeddingRsvp.Contracts.Responses.V1.EventFoodOptions;

namespace WeddingRsvp.Api.Mappings.V1;

public static class EventFoodOptionMapping
{
    public static EventFoodOption ToEntity(this CreateEventFoodOptionRequest request, Guid eventId)
    {
        return new EventFoodOption
        {
            Id = Guid.CreateVersion7(),
            EventId = eventId,
            FoodOptionId = request.FoodOptionId,
        };
    }
      
    public static EventFoodOptionGetResponse ToGetResponse(this EventFoodOption entity)
    {
        return new EventFoodOptionGetResponse(entity.Id, entity.EventId, entity.FoodOptionId);
    }

    private static EventFoodOptionResponse ToResponse(this EventFoodOption entity)
    {
        string foodOptionName = entity.FoodOption?.Name ?? string.Empty;
        FoodType foodOptionFoodType = entity.FoodOption?.FoodType ?? FoodType.Unknown;

        return new EventFoodOptionResponse(entity.Id, entity.EventId, entity.FoodOptionId, foodOptionName, foodOptionFoodType.ToString());
    }

    public static EventFoodOptionsResponse ToResponse(this PaginatedList<EventFoodOption> entities)
    {
        return new EventFoodOptionsResponse
        {
            Items = entities.Items.Select(ToResponse),
            PageNumber = entities.PageNumber,
            PageSize = entities.PageSize,
            TotalCount = entities.TotalCount,
            TotalPages = entities.TotalPages,
        };
    }
}
