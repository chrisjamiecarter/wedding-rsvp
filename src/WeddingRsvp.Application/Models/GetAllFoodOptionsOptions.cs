using WeddingRsvp.Application.Enums;

namespace WeddingRsvp.Application.Models;

public sealed class GetAllFoodOptionsOptions
{
    public Guid EventId { get; set; }
    public string? Name { get; set; }
    public FoodType? FoodType { get; set; }
    public string? SortField { get; set; }
    public SortOrder? SortOrder { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
