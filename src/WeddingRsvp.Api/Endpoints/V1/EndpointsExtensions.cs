using WeddingRsvp.Api.Endpoints.V1.Auth;

namespace WeddingRsvp.Api.Endpoints.V1;

public static class EndpointsExtensions
{
    public static IEndpointRouteBuilder MapApiV1Endpoints(this IEndpointRouteBuilder app)
    {
        app.MapAuthEndpoints();

        return app;
    }
}
