using System.Security.Claims;
using WeddingRsvp.Contracts.Responses.V1.Auth;

namespace WeddingRsvp.Api.Endpoints.V1.Auth;

public static partial class MeEndpoint
{
    public const string Name = "Me";

    public static IEndpointRouteBuilder MapMe(this IEndpointRouteBuilder app)
    {
        app.MapGet(Routes.Auth.Me,
            async (ClaimsPrincipal user) =>
            {
                var id = user.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
                var email = user.FindFirstValue(ClaimTypes.Email) ?? string.Empty;
                var isAdmin = bool.Parse(user.FindFirstValue("Admin") ?? "false");
                
                var response = new MeResponse(id, email, isAdmin);

                return await Task.FromResult(Results.Ok(response));
            })
            .WithName(Name)
            .Produces<MeResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .RequireAuthorization()
            .WithApiVersionSet(ApiVersioning.ApiVersionSet!)
            .HasApiVersion(1.0);

        return app;
    }
}
