﻿using WeddingRsvp.Api.Mappings.V1;
using WeddingRsvp.Application.Auth;
using WeddingRsvp.Application.Services;
using WeddingRsvp.Contracts.Requests.V1.Events;
using WeddingRsvp.Contracts.Responses.V1.Events;

namespace WeddingRsvp.Api.Endpoints.V1.Events;

public static class GetAllEventsEndpoint
{
    public const string Name = "GetEvents";

    public static IEndpointRouteBuilder MapGetAllEvents(this IEndpointRouteBuilder app)
    {
        app.MapGet(Routes.Events.GetAll,
            async ([AsParameters] GetAllEventsRequest request,
                   IEventService eventService,
                   HttpContext context,
                   CancellationToken cancellationToken) =>
            {
                var options = request.ToOptions();

                var events = await eventService.GetAllAsync(options, cancellationToken);

                return TypedResults.Ok(events.ToResponse());

            })
            .WithName(Name)
            .Produces<EventsResponse>(StatusCodes.Status200OK)
            .RequireAuthorization(AuthConstants.AdminPolicyName)
            .WithApiVersionSet(ApiVersioning.ApiVersionSet!)
            .HasApiVersion(1.0);
        //.CacheOutput("MovieCache");

        return app;
    }
}
