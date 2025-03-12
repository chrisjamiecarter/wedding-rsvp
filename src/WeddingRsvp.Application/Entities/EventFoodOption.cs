namespace WeddingRsvp.Application.Entities;

public sealed class EventFoodOption
{
    public required Guid Id { get; set; }

    public Guid EventId { get; set; }
    public Event? Event { get; set; }

    public Guid FoodOptionId { get; set; }
    public FoodOption? FoodOption { get; set; }
}
