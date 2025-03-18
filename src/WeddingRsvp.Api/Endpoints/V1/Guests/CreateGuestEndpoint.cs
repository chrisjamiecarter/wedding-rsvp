using Microsoft.AspNetCore.OutputCaching;
using WeddingRsvp.Api.Mappings.V1;
using WeddingRsvp.Application.Auth;
using WeddingRsvp.Application.Cache;
using WeddingRsvp.Application.Services;
using WeddingRsvp.Contracts.Requests.V1.Guests;
using WeddingRsvp.Contracts.Responses;
using WeddingRsvp.Contracts.Responses.V1.Guests;

namespace WeddingRsvp.Api.Endpoints.V1.Guests;

public static class CreateGuestEndpoint
{
    public const string Name = "CreateGuest";

    public static IEndpointRouteBuilder MapCreateGuest(this IEndpointRouteBuilder app)
    {
        app.MapPost(Routes.Invites.CreateGuest,
            async (Guid inviteId,
                   CreateGuestRequest request,
                   IGuestService guestService,
                   IOutputCacheStore outputCacheStore,
                   CancellationToken cancellationToken) =>
            {
                var guest = request.ToEntity(inviteId);

                await guestService.CreateAsync(guest, cancellationToken);

                await outputCacheStore.EvictByTagAsync(Policies.Guest.Tag, cancellationToken);

                var response = guest.ToResponse();
                return TypedResults.CreatedAtRoute(response, GetGuestEndpoint.Name, new { id = guest.Id });
            })
            .WithName(Name)
            .Produces<GuestResponse>(StatusCodes.Status201Created)
            .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .RequireAuthorization(AuthConstants.AdminPolicyName)
            .WithApiVersionSet(ApiVersioning.ApiVersionSet!)
            .HasApiVersion(1.0);

        return app;
    }
}
