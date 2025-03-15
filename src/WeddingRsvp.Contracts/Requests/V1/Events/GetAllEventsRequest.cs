namespace WeddingRsvp.Contracts.Requests.V1.Events;

public sealed class GetAllEventsRequest : PagedRequest
{
    public string? Name { get; init; }

    public string? Venue { get; init; }

    public DateOnly? DateFrom { get; init; }

    public DateOnly? DateTo { get; init; }

    public string? SortBy { get; init; }
}
