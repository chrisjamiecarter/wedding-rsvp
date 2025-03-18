using WeddingRsvp.Api.Mappings.V1;
using WeddingRsvp.Application.Auth;
using WeddingRsvp.Application.Cache;
using WeddingRsvp.Application.Services;
using WeddingRsvp.Contracts.Responses.V1.Guests;

namespace WeddingRsvp.Api.Endpoints.V1.Guests;

public static class GetGuestEndpoint
{
    public const string Name = "GetGuest";

    public static IEndpointRouteBuilder MapGetGuest(this IEndpointRouteBuilder app)
    {
        app.MapGet(Routes.Guests.Get,
            async (Guid id,
                   IGuestService guestService,
                   CancellationToken cancellationToken) =>
            {
                var guest = await guestService.GetByIdAsync(id, cancellationToken);

                return guest != null
                    ? TypedResults.Ok(guest.ToResponse())
                    : Results.NotFound();
            })
            .WithName(Name)
            .Produces<GuestResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization(AuthConstants.AdminPolicyName)
            .WithApiVersionSet(ApiVersioning.ApiVersionSet!)
            .HasApiVersion(1.0)
            .CacheOutput(Policies.Guest.Name);

        return app;
    }
}
