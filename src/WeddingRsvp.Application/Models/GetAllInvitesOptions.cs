using WeddingRsvp.Application.Enums;

namespace WeddingRsvp.Application.Models;

public sealed class GetAllInvitesOptions
{
    public Guid EventId { get; set; }
    public string? HouseholdName { get; set; }
    public string? Email { get; set; }
    public string? SortField { get; set; }
    public SortOrder? SortOrder { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
