using WeddingRsvp.Api.Mappings.V1;
using WeddingRsvp.Application.Auth;
using WeddingRsvp.Application.Cache;
using WeddingRsvp.Application.Services;
using WeddingRsvp.Contracts.Requests.V1.FoodOptions;
using WeddingRsvp.Contracts.Responses.V1.FoodOptions;

namespace WeddingRsvp.Api.Endpoints.V1.FoodOptions;

public static class GetAllFoodOptionsEndpoint
{
    public const string Name = "GetFoodOptions";

    public static IEndpointRouteBuilder MapGetAllFoodOptions(this IEndpointRouteBuilder app)
    {
        app.MapGet(Routes.Events.GetAllFoodOptions,
            async (Guid eventId,
                   [AsParameters] GetAllFoodOptionsRequest request,
                   IFoodOptionService foodOptionService,
                   CancellationToken cancellationToken) =>
            {
                var options = request.ToOptions(eventId);

                var foodOptions = await foodOptionService.GetAllAsync(options, cancellationToken);

                return TypedResults.Ok(foodOptions.ToResponse());

            })
            .WithName(Name)
            .Produces<FoodOptionsResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .RequireAuthorization(AuthConstants.AdminPolicyName)
            .WithApiVersionSet(ApiVersioning.ApiVersionSet!)
            .HasApiVersion(1.0)
            .CacheOutput(Policies.FoodOption.Name);

        return app;
    }
}
