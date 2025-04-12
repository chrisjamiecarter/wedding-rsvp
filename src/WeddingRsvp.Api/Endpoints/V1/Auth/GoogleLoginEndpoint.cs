using Microsoft.AspNetCore.Authentication;

namespace WeddingRsvp.Api.Endpoints.V1.Auth;

public static class GoogleLoginEndpoint
{
    public const string Name = "GoogleLogin";
    public const string Provider = "Google";

    public static IEndpointRouteBuilder MapGoogleLogin(this IEndpointRouteBuilder app)
    {
        app.MapGet(Routes.Auth.GoogleLogin,
            (
                HttpContext context,
                string returnUrl = "/"
            ) =>
            {
                var redirectUrl = $"{Routes.Auth.ExternalLogin}?returnUrl={Uri.EscapeDataString(returnUrl)}";
                //var redirectUrl = $"/signin-google?returnUrl={Uri.EscapeDataString(returnUrl)}";

                var properties = new AuthenticationProperties
                {
                    RedirectUri = redirectUrl,
                };
                properties.Items["LoginProvider"] = Provider;

                return TypedResults.Challenge(properties, [Provider]);
            })
            .WithName(Name)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .WithApiVersionSet(ApiVersioning.ApiVersionSet!)
            .HasApiVersion(1.0);

        return app;
    }
}
