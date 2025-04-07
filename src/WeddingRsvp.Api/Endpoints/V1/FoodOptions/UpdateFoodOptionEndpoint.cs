using Microsoft.AspNetCore.OutputCaching;
using WeddingRsvp.Api.Mappings.V1;
using WeddingRsvp.Application.Auth;
using WeddingRsvp.Application.Cache;
using WeddingRsvp.Application.Services;
using WeddingRsvp.Contracts.Requests.V1.FoodOptions;
using WeddingRsvp.Contracts.Responses;
using WeddingRsvp.Contracts.Responses.V1.FoodOptions;

namespace WeddingRsvp.Api.Endpoints.V1.FoodOptions;

public static class UpdateFoodOptionEndpoint
{
    public const string Name = "UpdateFoodOption";

    public static IEndpointRouteBuilder MapUpdateFoodOption(this IEndpointRouteBuilder app)
    {
        app.MapPut(Routes.FoodOptions.Update,
            async (Guid id,
                   UpdateFoodOptionRequest request,
                   IFoodOptionService foodOptionService,
                   IOutputCacheStore outputCacheStore,
                   CancellationToken cancellationToken) =>
            {
                var existingFoodOption = await foodOptionService.GetByIdAsync(id, cancellationToken);
                if (existingFoodOption is null)
                {
                    return Results.NotFound();
                }

                var foodOption = request.ToEntity(existingFoodOption);

                var updatedFoodOption = await foodOptionService.UpdateAsync(foodOption, cancellationToken);
                if (updatedFoodOption is null)
                {
                    return Results.NotFound();
                }

                await outputCacheStore.EvictByTagAsync(Policies.FoodOption.Tag, cancellationToken);

                var response = updatedFoodOption.ToResponse();
                return TypedResults.Ok(response);
            })
            .WithName(Name)
            .Produces<FoodOptionResponse>(StatusCodes.Status200OK)
            .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization(AuthConstants.AdminPolicyName)
            .WithApiVersionSet(ApiVersioning.ApiVersionSet!)
            .HasApiVersion(1.0);

        return app;
    }
}
