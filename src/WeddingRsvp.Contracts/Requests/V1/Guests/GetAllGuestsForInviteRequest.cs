namespace WeddingRsvp.Contracts.Requests.V1.Guests;

public sealed class GetAllGuestsForInviteRequest : PagedRequest
{
    public string? Name { get; init; }
    public string? RvspStatus { get; init; }
    public string? SortBy { get; init; }
}
