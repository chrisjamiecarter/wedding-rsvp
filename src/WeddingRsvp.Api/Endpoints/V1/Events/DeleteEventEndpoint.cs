using System.Security.Claims;
using Microsoft.AspNetCore.OutputCaching;
using WeddingRsvp.Application.Auth;
using WeddingRsvp.Application.Cache;
using WeddingRsvp.Application.Models;
using WeddingRsvp.Application.Services;

namespace WeddingRsvp.Api.Endpoints.V1.Events;

public static class DeleteEventEndpoint
{
    public const string Name = "DeleteEvent";

    public static IEndpointRouteBuilder MapDeleteEvent(this IEndpointRouteBuilder app)
    {
        app.MapDelete(Routes.Events.Delete,
            async (Guid id,
                   ClaimsPrincipal user,
                   IEventService eventService,
                   IOutputCacheStore outputCacheStore,
                   CancellationToken cancellationToken) =>
            {
                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId is null)
                {
                    return Results.Unauthorized();
                }

                var options = new DeleteEventOptions
                { 
                    EventId = id,
                    UserId = userId 
                };

                var deleted = await eventService.DeleteAsync(options, cancellationToken);
                if (!deleted)
                {
                    return Results.NotFound();
                }

                await outputCacheStore.EvictByTagAsync(Policies.Event.Tag, cancellationToken);

                return Results.NoContent();
            })
            .WithName(Name)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization(AuthConstants.AdminPolicyName)
            .WithApiVersionSet(ApiVersioning.ApiVersionSet!)
            .HasApiVersion(1.0);

        return app;
    }
}
