using WeddingRsvp.Application.Enums;

namespace WeddingRsvp.Application.Models;

public sealed class GuestRsvp
{
    public Guid Id { get; set; }
    public RsvpStatus RsvpStatus { get; set; }
    public Guid? MainFoodOptionId { get; set; }
    public Guid? DessertFoodOptionId { get; set; }
}
