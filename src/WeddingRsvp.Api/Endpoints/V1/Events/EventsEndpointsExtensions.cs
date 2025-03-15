namespace WeddingRsvp.Api.Endpoints.V1.Events;

public static class EventsEndpointsExtensions
{
    public static IEndpointRouteBuilder MapEventsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapCreateEvent();
        app.MapDeleteEvent();
        app.MapGetAllEvents();
        app.MapGetEvent();
        app.MapUpdateEvent();

        return app;
    }
}
