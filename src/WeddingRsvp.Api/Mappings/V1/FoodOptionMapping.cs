using WeddingRsvp.Application.Entities;
using WeddingRsvp.Application.Enums;
using WeddingRsvp.Application.Models;
using WeddingRsvp.Contracts.Requests.V1.FoodOptions;
using WeddingRsvp.Contracts.Responses.V1.FoodOptions;

namespace WeddingRsvp.Api.Mappings.V1;

public static class FoodOptionMapping
{
    public static FoodOption ToEntity(this CreateFoodOptionRequest request)
    {
        Enum.TryParse<FoodType>(request.FoodType, true, out var foodType);

        return new FoodOption
        {
            Id = Guid.CreateVersion7(),
            Name = request.Name,
            FoodType = foodType,
        };
    }

    public static FoodOption ToEntity(this UpdateFoodOptionRequest request, Guid id)
    {
        Enum.TryParse<FoodType>(request.FoodType, true, out var foodType);

        return new FoodOption
        {
            Id = id,
            Name = request.Name,
            FoodType = foodType,
        };
    }

    public static FoodOptionResponse ToResponse(this FoodOption entity)
    {
        return new FoodOptionResponse(entity.Id,
                                      entity.Name,
                                      entity.FoodType.ToString());
    }

    public static FoodOptionsResponse ToResponse(this PaginatedList<FoodOption> entities)
    {
        return new FoodOptionsResponse
        {
            Items = entities.Items.Select(ToResponse),
            PageNumber = entities.PageNumber,
            PageSize = entities.PageSize,
            TotalCount = entities.TotalCount,
            TotalPages = entities.TotalPages,
        };
    }
}
