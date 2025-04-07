using Microsoft.AspNetCore.OutputCaching;
using WeddingRsvp.Api.Mappings.V1;
using WeddingRsvp.Application.Auth;
using WeddingRsvp.Application.Cache;
using WeddingRsvp.Application.Services;
using WeddingRsvp.Contracts.Requests.V1.FoodOptions;
using WeddingRsvp.Contracts.Responses;
using WeddingRsvp.Contracts.Responses.V1.FoodOptions;

namespace WeddingRsvp.Api.Endpoints.V1.FoodOptions;

public static class CreateFoodOptionEndpoint
{
    public const string Name = "CreateFoodOption";

    public static IEndpointRouteBuilder MapCreateFoodOption(this IEndpointRouteBuilder app)
    {
        app.MapPost(Routes.Events.CreateFoodOption,
            async (Guid eventId,
                   CreateFoodOptionRequest request,
                   IFoodOptionService foodOptionService,
                   IOutputCacheStore outputCacheStore,
                   CancellationToken cancellationToken) =>
            {
                var foodOption = request.ToEntity(eventId);

                await foodOptionService.CreateAsync(foodOption, cancellationToken);

                await outputCacheStore.EvictByTagAsync(Policies.FoodOption.Tag, cancellationToken);

                var response = foodOption.ToResponse();
                return TypedResults.CreatedAtRoute(response, GetFoodOptionEndpoint.Name, new { id = foodOption.Id });
            })
            .WithName(Name)
            .Produces<FoodOptionResponse>(StatusCodes.Status201Created)
            .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .RequireAuthorization(AuthConstants.AdminPolicyName)
            .WithApiVersionSet(ApiVersioning.ApiVersionSet!)
            .HasApiVersion(1.0);

        return app;
    }
}
