using Microsoft.AspNetCore.OutputCaching;
using WeddingRsvp.Api.Mappings.V1;
using WeddingRsvp.Application.Auth;
using WeddingRsvp.Application.Cache;
using WeddingRsvp.Application.Services;
using WeddingRsvp.Contracts.Requests.V1.Events;
using WeddingRsvp.Contracts.Responses;
using WeddingRsvp.Contracts.Responses.V1.Events;

namespace WeddingRsvp.Api.Endpoints.V1.Events;

public static class CreateEventEndpoint
{
    public const string Name = "CreateEvent";

    public static IEndpointRouteBuilder MapCreateEvent(this IEndpointRouteBuilder app)
    {
        app.MapPost(Routes.Events.Create,
            async (CreateEventRequest request,
                   IEventService eventService,
                   IOutputCacheStore outputCacheStore,
                   CancellationToken cancellationToken) =>
            {
                var evnt = request.ToEntity();

                await eventService.CreateAsync(evnt, cancellationToken);

                await outputCacheStore.EvictByTagAsync(Policies.Event.Tag, cancellationToken);

                var response = evnt.ToResponse();
                return TypedResults.CreatedAtRoute(response, GetEventEndpoint.Name, new { id = evnt.Id });
            })
            .WithName(Name)
            .Produces<EventResponse>(StatusCodes.Status201Created)
            .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .RequireAuthorization(AuthConstants.AdminPolicyName)
            .WithApiVersionSet(ApiVersioning.ApiVersionSet!)
            .HasApiVersion(1.0);

        return app;
    }
}
