namespace WeddingRsvp.Api.Endpoints.V1.Events;

public static class EventsEndpointsExtensions
{
    public static IEndpointRouteBuilder MapEventsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapCreateEvent();
        app.MapGetAllEvents();
        app.MapGetEvent();

        return app;
    }
}
