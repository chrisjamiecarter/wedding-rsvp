using WeddingRsvp.Api.Mappings.V1;
using WeddingRsvp.Application.Auth;
using WeddingRsvp.Application.Cache;
using WeddingRsvp.Application.Services;
using WeddingRsvp.Contracts.Responses.V1.FoodOptions;

namespace WeddingRsvp.Api.Endpoints.V1.FoodOptions;

public static class GetFoodOptionEndpoint
{
    public const string Name = "GetFoodOption";

    public static IEndpointRouteBuilder MapGetFoodOption(this IEndpointRouteBuilder app)
    {
        app.MapGet(Routes.FoodOptions.Get,
            async (Guid id,
                   IFoodOptionService foodOptionService,
                   CancellationToken cancellationToken) =>
            {
                var foodOption = await foodOptionService.GetByIdAsync(id, cancellationToken);

                return foodOption != null
                    ? TypedResults.Ok(foodOption.ToResponse())
                    : Results.NotFound();
            })
            .WithName(Name)
            .Produces<FoodOptionResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization(AuthConstants.AdminPolicyName)
            .WithApiVersionSet(ApiVersioning.ApiVersionSet!)
            .HasApiVersion(1.0)
            .CacheOutput(Policies.FoodOption.Name);

        return app;
    }
}
