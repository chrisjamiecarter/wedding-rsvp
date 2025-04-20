namespace WeddingRsvp.Contracts.Requests.V1.Invites;

public sealed class GetAllInvitesRequest : PagedRequest
{
    public string? Email { get; init; }
    public string? HouseholdName { get; init; }
    public string? SortBy { get; init; }
}