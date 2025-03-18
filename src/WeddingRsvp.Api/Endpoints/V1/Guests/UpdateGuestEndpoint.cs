using Microsoft.AspNetCore.OutputCaching;
using WeddingRsvp.Api.Mappings.V1;
using WeddingRsvp.Application.Auth;
using WeddingRsvp.Application.Cache;
using WeddingRsvp.Application.Services;
using WeddingRsvp.Contracts.Requests.V1.Guests;
using WeddingRsvp.Contracts.Responses;
using WeddingRsvp.Contracts.Responses.V1.Guests;

namespace WeddingRsvp.Api.Endpoints.V1.Guests;

public static class UpdateGuestEndpoint
{
    public const string Name = "UpdateGuest";

    public static IEndpointRouteBuilder MapUpdateGuest(this IEndpointRouteBuilder app)
    {
        app.MapPut(Routes.Guests.Update,
            async (Guid id,
                   UpdateGuestRequest request,
                   IGuestService guestService,
                   IOutputCacheStore outputCacheStore,
                   CancellationToken cancellationToken) =>
            {
                var existingGuest = await guestService.GetByIdAsync(id, cancellationToken);
                if (existingGuest is null)
                {
                    return Results.NotFound();
                }

                var guest = request.ToEntity(existingGuest);

                var updatedGuest = await guestService.UpdateAsync(guest, cancellationToken);
                if (updatedGuest is null)
                {
                    return Results.NotFound();
                }

                await outputCacheStore.EvictByTagAsync(Policies.Guest.Tag, cancellationToken);

                var response = updatedGuest.ToResponse();
                return TypedResults.Ok(response);
            })
            .WithName(Name)
            .Produces<GuestResponse>(StatusCodes.Status200OK)
            .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization(AuthConstants.AdminPolicyName)
            .WithApiVersionSet(ApiVersioning.ApiVersionSet!)
            .HasApiVersion(1.0);

        return app;
    }
}
