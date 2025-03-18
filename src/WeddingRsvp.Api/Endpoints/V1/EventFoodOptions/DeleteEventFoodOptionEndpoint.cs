using Microsoft.AspNetCore.OutputCaching;
using WeddingRsvp.Application.Auth;
using WeddingRsvp.Application.Cache;
using WeddingRsvp.Application.Services;

namespace WeddingRsvp.Api.Endpoints.V1.EventFoodOptions;

public static class DeleteEventFoodOptionEndpoint
{
    public const string Name = "DeleteEventFoodOption";

    public static IEndpointRouteBuilder MapDeleteEventFoodOption(this IEndpointRouteBuilder app)
    {
        app.MapDelete(Routes.Events.DeleteFoodOption,
            async (Guid eventId,
                   Guid foodOptionId,
                   IEventFoodOptionService eventFoodOptionService,
                   IOutputCacheStore outputCacheStore,
                   CancellationToken cancellationToken) =>
            {
                var eventFoodOption = await eventFoodOptionService.GetByEventIdAndFoodOptionIdAsync(eventId, foodOptionId, cancellationToken);
                if (eventFoodOption is null)
                {
                    return Results.NotFound();
                }

                var deleted = await eventFoodOptionService.DeleteByIdAsync(eventFoodOption.Id, cancellationToken);
                if (!deleted)
                {
                    return Results.NotFound();
                }

                await outputCacheStore.EvictByTagAsync(Policies.EventFoodOption.Tag, cancellationToken);

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
