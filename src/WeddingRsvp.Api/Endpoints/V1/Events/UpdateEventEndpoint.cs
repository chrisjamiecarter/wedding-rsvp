using System.Security.Claims;
using Microsoft.AspNetCore.OutputCaching;
using WeddingRsvp.Api.Mappings.V1;
using WeddingRsvp.Application.Auth;
using WeddingRsvp.Application.Cache;
using WeddingRsvp.Application.Services;
using WeddingRsvp.Contracts.Requests.V1.Events;
using WeddingRsvp.Contracts.Responses;
using WeddingRsvp.Contracts.Responses.V1.Events;

namespace WeddingRsvp.Api.Endpoints.V1.Events;

public static class UpdateEventEndpoint
{
    public const string Name = "UpdateEvent";

    public static IEndpointRouteBuilder MapUpdateEvent(this IEndpointRouteBuilder app)
    {
        app.MapPut(Routes.Events.Update,
            async (Guid id,
                   ClaimsPrincipal user,
                   UpdateEventRequest request,
                   IEventService eventService,
                   IOutputCacheStore outputCacheStore,
                   CancellationToken cancellationToken) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId is null)
                {
                    return Results.Unauthorized();
                }

                var evnt = request.ToEntity(id, userId);

                var updatedEvent = await eventService.UpdateAsync(evnt, cancellationToken);
                if (updatedEvent is null)
                {
                    return Results.NotFound();
                }

                await outputCacheStore.EvictByTagAsync(Policies.Event.Tag, cancellationToken);

                var response = updatedEvent.ToResponse();
                return TypedResults.Ok(response);
            })
            .WithName(Name)
            .Produces<EventResponse>(StatusCodes.Status200OK)
            .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization(AuthConstants.AdminPolicyName)
            .WithApiVersionSet(ApiVersioning.ApiVersionSet!)
            .HasApiVersion(1.0);

        return app;
    }
}
