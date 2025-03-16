namespace WeddingRsvp.Contracts.Requests.V1.FoodOptions;

public sealed class GetAllFoodOptionsRequest : PagedRequest
{
    public string? Name { get; init; }

    public string? FoodType { get; init; }

    public string? SortBy { get; init; }
}
