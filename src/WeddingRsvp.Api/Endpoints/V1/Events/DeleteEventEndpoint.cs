using WeddingRsvp.Application.Auth;
using WeddingRsvp.Application.Services;

namespace WeddingRsvp.Api.Endpoints.V1.Events;

public static class DeleteEventEndpoint
{
    public const string Name = "DeleteEvent";

    public static IEndpointRouteBuilder MapDeleteEvent(this IEndpointRouteBuilder app)
    {
        app.MapDelete(Routes.Events.Delete,
            async (Guid id,
                   IEventService eventService,
                   CancellationToken cancellationToken) =>
            {
                var deleted = await eventService.DeleteByIdAsync(id, cancellationToken);
                if (!deleted)
                {
                    return Results.NotFound();
                }

                // TODO evict cache.

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
