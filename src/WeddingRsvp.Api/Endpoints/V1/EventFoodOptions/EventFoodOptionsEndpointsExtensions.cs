namespace WeddingRsvp.Api.Endpoints.V1.EventFoodOptions;

public static class EventFoodOptionsEndpointsExtensions
{
    public static IEndpointRouteBuilder MapEventFoodOptionsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapCreateEventFoodOption();
        app.MapDeleteEventFoodOption();
        app.MapGetAllEventFoodOptions();
        app.MapGetEventFoodOption();

        return app;
    }
}
