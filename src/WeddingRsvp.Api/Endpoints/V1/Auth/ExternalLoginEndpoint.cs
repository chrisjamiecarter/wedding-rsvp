using WeddingRsvp.Application.Services;

namespace WeddingRsvp.Api.Endpoints.V1.Auth;

public static class ExternalLoginEndpoint
{
    public const string Name = "ExternalLogin";

    public static IEndpointRouteBuilder MapExternalLogin(this IEndpointRouteBuilder app)
    {
        app.MapGet(Routes.Auth.ExternalLogin,
            async (HttpContext context,
                   string returnUrl,
                   IAuthService authService,
                   CancellationToken cancellationToken) =>
            {
                var result = await authService.ExternalLoginAsync();

                return result.IsSuccess
                    ? Results.Redirect(returnUrl ?? "/")
                    : Results.Redirect("/login?error=ExternalLoginFailed");
            })
            .WithName(Name)
            .Produces(StatusCodes.Status302Found)
            .WithApiVersionSet(ApiVersioning.ApiVersionSet!)
            .HasApiVersion(1.0);

        return app;
    }
}
