namespace WeddingRsvp.Contracts.Requests.V1.Guests;

public sealed class GetAllGuestsForEventRequest : PagedRequest
{
    public string? Name { get; init; }
    public string? RvspStatus { get; init; }
    public string? HouseholdName { get; init; }
    public string? SortBy { get; init; }
}
