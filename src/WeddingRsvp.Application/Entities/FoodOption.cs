using WeddingRsvp.Application.Enums;

namespace WeddingRsvp.Application.Entities;

public sealed class FoodOption
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public FoodType FoodType { get; set; } = FoodType.Unknown;

    public ICollection<EventFoodOption>? EventFoodOptions { get; }
}
