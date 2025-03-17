namespace WeddingRsvp.Contracts.Requests.V1.EventFoodOptions;

public sealed class GetAllEventFoodOptionsRequest : PagedRequest
{
    public string? FoodType { get; init; }
    public string? SortBy { get; init; }
}
