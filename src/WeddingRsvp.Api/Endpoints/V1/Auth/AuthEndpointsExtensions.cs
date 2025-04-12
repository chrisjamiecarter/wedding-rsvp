namespace WeddingRsvp.Api.Endpoints.V1.Auth;

public static class AuthEndpointsExtensions
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapExternalLogin();
        app.MapGoogleLogin();
        app.MapLogout(); // Identity doesn't map a logout endpoint.
        app.MapMe();

        return app;
    }
}
