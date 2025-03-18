namespace WeddingRsvp.Api.Endpoints.V1.Guests;

public static class GuestsEndpointsExtensions
{
    public static IEndpointRouteBuilder MapGuestsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapCreateGuest();
        app.MapDeleteGuest();
        app.MapGetAllGuestsForEvent();
        app.MapGetAllGuestsForInvite();
        app.MapGetGuest();
        app.MapUpdateGuest();

        return app;
    }
}
