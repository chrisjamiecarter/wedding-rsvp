using WeddingRsvp.Api.Mappings.V1;
using WeddingRsvp.Application.Auth;
using WeddingRsvp.Application.Cache;
using WeddingRsvp.Application.Services;
using WeddingRsvp.Contracts.Requests.V1.EventFoodOptions;
using WeddingRsvp.Contracts.Responses.V1.EventFoodOptions;

namespace WeddingRsvp.Api.Endpoints.V1.EventFoodOptions;

public static class GetAllEventFoodOptionsEndpoint
{
    public const string Name = "GetEventFoodOptions";

    public static IEndpointRouteBuilder MapGetAllEventFoodOptions(this IEndpointRouteBuilder app)
    {
        app.MapGet(Routes.Events.GetAllFoodOptions,
            async (Guid eventId,
                   [AsParameters] GetAllEventFoodOptionsRequest request,
                   IEventFoodOptionService eventFoodOptionService,
                   CancellationToken cancellationToken) =>
            {
                var options = request.ToOptions(eventId);

                var eventFoodOptions = await eventFoodOptionService.GetAllAsync(options, cancellationToken);

                return TypedResults.Ok(eventFoodOptions.ToResponse());

            })
            .WithName(Name)
            .Produces<EventFoodOptionsResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .RequireAuthorization(AuthConstants.AdminPolicyName)
            .WithApiVersionSet(ApiVersioning.ApiVersionSet!)
            .HasApiVersion(1.0)
            .CacheOutput(Policies.EventFoodOption.Name);

        return app;
    }
}
