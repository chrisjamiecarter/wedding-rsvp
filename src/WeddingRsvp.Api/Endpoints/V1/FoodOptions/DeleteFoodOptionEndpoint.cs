using Microsoft.AspNetCore.OutputCaching;
using WeddingRsvp.Application.Auth;
using WeddingRsvp.Application.Cache;
using WeddingRsvp.Application.Services;

namespace WeddingRsvp.Api.Endpoints.V1.FoodOptions;

public static class DeleteFoodOptionEndpoint
{
    public const string Name = "DeleteFoodOption";

    public static IEndpointRouteBuilder MapDeleteFoodOption(this IEndpointRouteBuilder app)
    {
        app.MapDelete(Routes.FoodOptions.Delete,
            async (Guid id,
                   IFoodOptionService foodOptionService,
                   IOutputCacheStore outputCacheStore,
                   CancellationToken cancellationToken) =>
            {
                var deleted = await foodOptionService.DeleteByIdAsync(id, cancellationToken);
                if(!deleted)
                {
                    return Results.NotFound();
                }

                await outputCacheStore.EvictByTagAsync(Policies.FoodOption.Tag, cancellationToken);

                return Results.NoContent();
            })
            .WithName(Name)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization(AuthConstants.AdminPolicyName)
            .WithApiVersionSet(ApiVersioning.ApiVersionSet!)
            .HasApiVersion(1.0);

        return app;
    }
}
