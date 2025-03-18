using WeddingRsvp.Api.Mappings.V1;
using WeddingRsvp.Application.Auth;
using WeddingRsvp.Application.Cache;
using WeddingRsvp.Application.Services;
using WeddingRsvp.Contracts.Requests.V1.Guests;
using WeddingRsvp.Contracts.Responses.V1.Guests;

namespace WeddingRsvp.Api.Endpoints.V1.Guests;

public static class GetAllGuestsForEventEndpoint
{
    public const string Name = "GetGuestsForEvent";

    public static IEndpointRouteBuilder MapGetAllGuestsForEvent(this IEndpointRouteBuilder app)
    {
        app.MapGet(Routes.Events.GetAllGuests,
            async (Guid eventId,
                   [AsParameters] GetAllGuestsForEventRequest request,
                   IGuestService guestService,
                   CancellationToken cancellationToken) =>
            {
                var options = request.ToOptions(eventId);

                var guests = await guestService.GetAllGuestsForEventOptionsAsync(options, cancellationToken);

                return TypedResults.Ok(guests.ToDetailsResponse());
            })
            .WithName(Name)
            .Produces<GuestDetailsResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .RequireAuthorization(AuthConstants.AdminPolicyName)
            .WithApiVersionSet(ApiVersioning.ApiVersionSet!)
            .HasApiVersion(1.0)
            .CacheOutput(Policies.Guest.Name);

        return app;
    }
}
