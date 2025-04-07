using WeddingRsvp.Api.Endpoints.V1.Auth;
using WeddingRsvp.Api.Endpoints.V1.Events;
using WeddingRsvp.Api.Endpoints.V1.FoodOptions;
using WeddingRsvp.Api.Endpoints.V1.Guests;
using WeddingRsvp.Api.Endpoints.V1.Invites;

namespace WeddingRsvp.Api.Endpoints.V1;

public static class EndpointsExtensions
{
    public static IEndpointRouteBuilder MapApiV1Endpoints(this IEndpointRouteBuilder app)
    {
        app.MapAuthEndpoints();
        app.MapEventsEndpoints();
        app.MapFoodOptionsEndpoints();
        app.MapInvitesEndpoints();
        app.MapGuestsEndpoints();

        return app;
    }
}
