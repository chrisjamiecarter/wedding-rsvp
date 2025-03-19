using WeddingRsvp.Api.Mappings.V1;
using WeddingRsvp.Application.Services;
using WeddingRsvp.Contracts.Requests.V1.Auth;
using WeddingRsvp.Contracts.Responses.V1.Auth;

namespace WeddingRsvp.Api.Endpoints.V1.Auth;

public static class RefreshEndpoint
{
    public const string Name = "Refresh";

    public static IEndpointRouteBuilder MapRefresh(this IEndpointRouteBuilder app)
    {
        app.MapPost(Routes.Auth.Refresh,
            async (RefreshRequest request,
                   IAuthService authService,
                   CancellationToken cancellationToken) =>
            {
                var result = await authService.RefreshAsync(request.RefreshTokenValue, cancellationToken);
                if (result.IsFailure)
                {
                    return Results.BadRequest("Invalid refresh attempt");
                }

                var response = result.Value.ToResponse();
                return Results.Ok(response);
            })
            .WithName(Name)
            .Produces<AuthResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithApiVersionSet(ApiVersioning.ApiVersionSet!)
            .HasApiVersion(1.0);

        return app;
    }
}
