using WeddingRsvp.Api.Mappings.V1;
using WeddingRsvp.Application.Auth;
using WeddingRsvp.Application.Cache;
using WeddingRsvp.Application.Services;
using WeddingRsvp.Contracts.Responses.V1.EventFoodOptions;

namespace WeddingRsvp.Api.Endpoints.V1.EventFoodOptions;

public static class GetEventFoodOptionEndpoint
{
    public const string Name = "GetEventFoodOption";

    public static IEndpointRouteBuilder MapGetEventFoodOption(this IEndpointRouteBuilder app)
    {
        app.MapGet(Routes.EventFoodOptions.Get,
            async (Guid id,
                   IEventFoodOptionService eventFoodOptionService,
                   CancellationToken cancellationToken) =>
            {
                var eventFoodOption = await eventFoodOptionService.GetByIdAsync(id, cancellationToken);

                return eventFoodOption != null
                    ? TypedResults.Ok(eventFoodOption.ToGetResponse())
                    : Results.NotFound();
            })
            .WithName(Name)
            .Produces<EventFoodOptionGetResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization(AuthConstants.AdminPolicyName)
            .WithApiVersionSet(ApiVersioning.ApiVersionSet!)
            .HasApiVersion(1.0)
            .CacheOutput(Policies.EventFoodOption.Name);

        return app;
    }
}
