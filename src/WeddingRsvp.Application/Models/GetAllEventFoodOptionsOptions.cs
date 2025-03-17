using WeddingRsvp.Application.Enums;

namespace WeddingRsvp.Application.Models;

public sealed class GetAllEventFoodOptionsOptions
{
    public Guid EventId { get; set; }
    public FoodType? FoodType { get; set; }
    public string? SortField { get; set; }
    public SortOrder? SortOrder { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
