namespace WeddingRsvp.Api.Endpoints.V1.Auth;

public static class AuthEndpointsExtensions
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapLogin();
        app.MapRefresh();
        app.MapRegister();

        return app;
    }
}
