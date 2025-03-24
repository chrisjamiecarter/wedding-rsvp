namespace WeddingRsvp.Api.Endpoints.V1.Auth;

public static class AuthEndpointsExtensions
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        // Identity doesnt map a logout endpoint.
        app.MapLogout();

        return app;
    }
}
