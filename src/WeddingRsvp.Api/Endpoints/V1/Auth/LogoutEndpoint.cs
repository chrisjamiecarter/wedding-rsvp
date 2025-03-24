using Microsoft.AspNetCore.Mvc;
using WeddingRsvp.Application.Services;

namespace WeddingRsvp.Api.Endpoints.V1.Auth;

public static class LogoutEndpoint
{
    public const string Name = "Logout";

    public static IEndpointRouteBuilder MapLogout(this IEndpointRouteBuilder app)
    {
        app.MapPost(Routes.Auth.Logout,
            async ([FromBody] object empty,
                   IAuthService authService,
                   CancellationToken cancellationToken) =>
            {
                if (empty is not null)
                {
                    await authService.LogoutAsync(cancellationToken);
                    return Results.Ok();
                }

                return Results.NotFound();
            })
            .WithName(Name)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization()
            .WithApiVersionSet(ApiVersioning.ApiVersionSet!)
            .HasApiVersion(1.0);

        return app;
    }
}
