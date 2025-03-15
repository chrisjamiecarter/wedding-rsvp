using WeddingRsvp.Api.Endpoints.V1.Auth;
using WeddingRsvp.Api.Endpoints.V1.Events;

namespace WeddingRsvp.Api.Endpoints.V1;

public static class EndpointsExtensions
{
    public static IEndpointRouteBuilder MapApiV1Endpoints(this IEndpointRouteBuilder app)
    {
        app.MapAuthEndpoints();
        app.MapEventsEndpoints();

        return app;
    }
}
