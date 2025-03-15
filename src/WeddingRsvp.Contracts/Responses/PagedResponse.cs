namespace WeddingRsvp.Contracts.Responses;

public record PagedResponse<TResponse>
{
    public required IEnumerable<TResponse> Items { get; init; } = [];

    public required int PageNumber { get; init; }

    public required int PageSize { get; init; }

    public required int TotalCount { get; init; }

    public required int TotalPages { get; init; }

    public bool HasNextPage => PageNumber < TotalPages;

    public bool HasPreviousPage => PageNumber > 1;

    public bool IsFirstPage => PageNumber == 1;

    public bool IsLastPage => PageNumber == TotalPages;
}
