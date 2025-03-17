using Microsoft.AspNetCore.OutputCaching;
using WeddingRsvp.Api.Mappings.V1;
using WeddingRsvp.Application.Auth;
using WeddingRsvp.Application.Cache;
using WeddingRsvp.Application.Services;
using WeddingRsvp.Contracts.Requests.V1.EventFoodOptions;
using WeddingRsvp.Contracts.Responses;
using WeddingRsvp.Contracts.Responses.V1.EventFoodOptions;

namespace WeddingRsvp.Api.Endpoints.V1.EventFoodOptions;

public static class CreateEventFoodOptionEndpoint
{
    public const string Name = "CreateEventFoodOption";

    public static IEndpointRouteBuilder MapCreateEventFoodOption(this IEndpointRouteBuilder app)
    {
        app.MapPost(Routes.Events.CreateFoodOption,
            async (Guid eventId,
                   CreateEventFoodOptionRequest request,
                   IEventFoodOptionService eventFoodOptionService,
                   IOutputCacheStore outputCacheStore,
                   CancellationToken cancellationToken) =>
            {
                var eventFoodOption = request.ToEntity(eventId);

                await eventFoodOptionService.CreateAsync(eventFoodOption, cancellationToken);

                await outputCacheStore.EvictByTagAsync(Policies.EventFoodOption.Tag, cancellationToken);

                var response = eventFoodOption.ToGetResponse();
                return TypedResults.CreatedAtRoute(response, GetEventFoodOptionEndpoint.Name, new { id = response.Id });
            })
            .WithName(Name)
            .Produces<EventFoodOptionGetResponse>(StatusCodes.Status201Created)
            .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .RequireAuthorization(AuthConstants.AdminPolicyName)
            .WithApiVersionSet(ApiVersioning.ApiVersionSet!)
            .HasApiVersion(1.0);

        return app;
    }
}
