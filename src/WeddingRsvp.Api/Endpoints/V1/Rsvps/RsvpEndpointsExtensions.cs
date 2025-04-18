namespace WeddingRsvp.Api.Endpoints.V1.Rsvps;

public static class RsvpEndpointsExtensions
{
    public static IEndpointRouteBuilder MapRsvpsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGetRsvp();
        app.MapSubmitRsvp();
        return app;
    }
}
