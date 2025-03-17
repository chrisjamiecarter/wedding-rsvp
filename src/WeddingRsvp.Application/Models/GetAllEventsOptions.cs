using WeddingRsvp.Application.Enums;

namespace WeddingRsvp.Application.Models;

public sealed class GetAllEventsOptions
{
    public string? Name { get; set; }
    public string? Venue { get; set; }
    public DateOnly? DateFrom { get; set; }
    public DateOnly? DateTo { get; set; }
    public string? SortField { get; set; }
    public SortOrder? SortOrder { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
