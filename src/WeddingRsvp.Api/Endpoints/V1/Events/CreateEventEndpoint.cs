﻿using WeddingRsvp.Api.Mappings.V1;
using WeddingRsvp.Application.Auth;
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
            async ([AsParameters] CreateEventRequest request,
                   IEventService eventService,
                   CancellationToken cancellationToken) =>
            {
                var evnt = request.ToEntity();

                await eventService.CreateAsync(evnt, cancellationToken);

                // TODO evict cache.

                var response = evnt.ToResponse();
                return TypedResults.CreatedAtRoute(response, GetEventEndpoint.Name, new {id = evnt.Id});
            })
            .WithName(Name)
            .Produces<EventResponse>(StatusCodes.Status201Created)
            .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest)
            .RequireAuthorization(AuthConstants.AdminPolicyName)
            .WithApiVersionSet(ApiVersioning.ApiVersionSet!)
            .HasApiVersion(1.0);

        return app;
    }
}
