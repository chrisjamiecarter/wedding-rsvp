using WeddingRsvp.Api.Mappings.V1;
using WeddingRsvp.Application.Auth;
using WeddingRsvp.Application.Cache;
using WeddingRsvp.Application.Services;
using WeddingRsvp.Contracts.Responses.V1.Events;

namespace WeddingRsvp.Api.Endpoints.V1.Events;

public static class GetEventEndpoint
{
    public const string Name = "GetEvent";

    public static IEndpointRouteBuilder MapGetEvent(this IEndpointRouteBuilder app)
    {
        app.MapGet(Routes.Events.Get,
            async (Guid id,
                   IEventService eventService,
                   CancellationToken cancellationToken) =>
            {
                var evnt = await eventService.GetByIdAsync(id, cancellationToken);

                return evnt != null
                    ? TypedResults.Ok(evnt.ToResponse())
                    : Results.NotFound();
            })
            .WithName(Name)
            .Produces<EventResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization(AuthConstants.AdminPolicyName)
            .WithApiVersionSet(ApiVersioning.ApiVersionSet!)
            .HasApiVersion(1.0)
            .CacheOutput(Policies.Event.Name);

        return app;
    }
}
