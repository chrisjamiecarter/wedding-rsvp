using System.Security.Claims;
using WeddingRsvp.Api.Mappings.V1;
using WeddingRsvp.Application.Services;
using WeddingRsvp.Contracts.Responses.V1.Auth;

namespace WeddingRsvp.Api.Endpoints.V1.Auth;

public static partial class MeEndpoint
{
    public const string Name = "Me";

    public static IEndpointRouteBuilder MapMe(this IEndpointRouteBuilder app)
    {
        app.MapGet(Routes.Auth.Me,
            async (ClaimsPrincipal user,
                   IAuthService authService) =>
            {
                var id = user.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

                var applicationUser = await authService.GetByIdAsync(id);
                var isAdmin = await authService.IsAdminAsync(applicationUser);

                return TypedResults.Ok(applicationUser.ToResponse(isAdmin));
            })
            .WithName(Name)
            .Produces<MeResponse>(StatusCodes.Status200OK)
            .WithApiVersionSet(ApiVersioning.ApiVersionSet!)
            .HasApiVersion(1.0);

        return app;
    }
}
