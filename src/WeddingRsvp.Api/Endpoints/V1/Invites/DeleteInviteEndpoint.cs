using Microsoft.AspNetCore.OutputCaching;
using WeddingRsvp.Application.Auth;
using WeddingRsvp.Application.Cache;
using WeddingRsvp.Application.Services;

namespace WeddingRsvp.Api.Endpoints.V1.Invites;

public static class DeleteInviteEndpoint
{
    public const string Name = "DeleteInvite";

    public static IEndpointRouteBuilder MapDeleteInvite(this IEndpointRouteBuilder app)
    {
        app.MapDelete(Routes.Invites.Delete,
            async (Guid id,
                   IInviteService inviteService,
                   IOutputCacheStore outputCacheStore,
                   CancellationToken cancellationToken) =>
            {
                var deleted = await inviteService.DeleteByIdAsync(id, cancellationToken);
                if (!deleted)
                {
                    return Results.NotFound();
                }

                await outputCacheStore.EvictByTagAsync(Policies.Invite.Tag, cancellationToken);

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
