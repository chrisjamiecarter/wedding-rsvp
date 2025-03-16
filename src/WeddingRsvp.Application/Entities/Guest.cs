using WeddingRsvp.Application.Enums;

namespace WeddingRsvp.Application.Entities;

public sealed class Guest
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public RsvpStatus RsvpStatus { get; set; } = RsvpStatus.Unknown;

    public Guid InviteId { get; set; }
    public Invite? Invite { get; set; }

    public Guid? DessertFoodOptionId { get; set; }
    public FoodOption? DessertFoodOption { get; set; }

    public Guid? MainFoodOptionId { get; set; }
    public FoodOption? MainFoodOption { get; set; }
}
