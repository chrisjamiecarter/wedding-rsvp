using Microsoft.AspNetCore.OutputCaching;
using WeddingRsvp.Api.Mappings.V1;
using WeddingRsvp.Application.Auth;
using WeddingRsvp.Application.Cache;
using WeddingRsvp.Application.Services;
using WeddingRsvp.Contracts.Requests.V1.Invites;
using WeddingRsvp.Contracts.Responses;
using WeddingRsvp.Contracts.Responses.V1.Invites;

namespace WeddingRsvp.Api.Endpoints.V1.Invites;

public static class UpdateInviteEndpoint
{
    public const string Name = "UpdateInvite";

    public static IEndpointRouteBuilder MapUpdateInvite(this IEndpointRouteBuilder app)
    {
        app.MapPut(Routes.Invites.Update,
            async (Guid id,
                   UpdateInviteRequest request,
                   IInviteService inviteService,
                   IOutputCacheStore outputCacheStore,
                   CancellationToken cancellationToken) =>
            {
                var existingInvite = await inviteService.GetByIdAsync(id, cancellationToken);
                if (existingInvite is null)
                {
                    return Results.NotFound();
                }

                var invite = request.ToEntity(existingInvite);

                var updatedInvite = await inviteService.UpdateAsync(invite, cancellationToken);
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
            .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization(AuthConstants.AdminPolicyName)
            .WithApiVersionSet(ApiVersioning.ApiVersionSet!)
            .HasApiVersion(1.0);

        return app;
    }
}
