namespace WeddingRsvp.Contracts.Requests;

public class PagedRequest
{
    public const int DefaultPageNumber = 1;
    public const int DefaultPageSize = 10;

    public int? PageNumber { get; init; } = DefaultPageNumber;

    public int? PageSize { get; init; } = DefaultPageSize;
}
