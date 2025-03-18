using Microsoft.AspNetCore.OutputCaching;
using WeddingRsvp.Api.Mappings.V1;
using WeddingRsvp.Application.Auth;
using WeddingRsvp.Application.Cache;
using WeddingRsvp.Application.Services;
using WeddingRsvp.Contracts.Responses.V1.Invites;

namespace WeddingRsvp.Api.Endpoints.V1.Invites;

public static class GenerateTokenForInviteEndpoint
{
    public const string Name = "GenerateTokenForInvite";

    public static IEndpointRouteBuilder MapGenerateTokenForInvite(this IEndpointRouteBuilder app)
    {
        app.MapPost(Routes.Invites.GenerateToken,
            async (Guid id,
                   IInviteService inviteService,
                   IOutputCacheStore outputCacheStore,
                   CancellationToken cancellationToken) =>
            {
                var updatedInvite = await inviteService.GenerateTokenAsync(id, cancellationToken);
                if (updatedInvite is null)
                {
                    return Results.NotFound();
                }

                await outputCacheStore.EvictByTagAsync(Policies.Invite.Tag, cancellationToken);

                var response = updatedInvite.ToResponse();
                return TypedResults.Ok(response);
            })
            .WithName(Name)
            .Produces<InviteResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization(AuthConstants.AdminPolicyName)
            .WithApiVersionSet(ApiVersioning.ApiVersionSet!)
            .HasApiVersion(1.0);

        return app;
    }
}
