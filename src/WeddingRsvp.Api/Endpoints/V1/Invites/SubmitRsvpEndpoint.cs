using Microsoft.AspNetCore.OutputCaching;
using WeddingRsvp.Api.Mappings.V1;
using WeddingRsvp.Application.Auth;
using WeddingRsvp.Application.Cache;
using WeddingRsvp.Application.Services;
using WeddingRsvp.Contracts.Requests.V1.Invites;
using WeddingRsvp.Contracts.Responses;
using WeddingRsvp.Contracts.Responses.V1.Invites;

namespace WeddingRsvp.Api.Endpoints.V1.Invites;

public static class SubmitRsvpEndpoint
{
    public const string Name = "SubmitRsvp";

    public static IEndpointRouteBuilder MapSubmitRsvp(this IEndpointRouteBuilder app)
    {
        app.MapPost(Routes.Invites.SubmitRsvp,
            async (Guid id,
                   SubmitRsvpRequest request,
                   IInviteService inviteService,
                   IOutputCacheStore outputCacheStore,
                   CancellationToken cancellationToken) =>
            {
                var invite = await inviteService.GetByIdAsync(id, cancellationToken);
                if (invite is null)
                {
                    return Results.NotFound();
                }

                var inviteRsvp = request.ToModel(id);

                var updatedInvite = await inviteService.SubmitRsvp(inviteRsvp, cancellationToken);
                if (updatedInvite == null)
                {
                    return Results.NotFound();
                }

                await outputCacheStore.EvictByTagAsync(Policies.Invite.Tag, cancellationToken);
                await outputCacheStore.EvictByTagAsync(Policies.Guest.Tag, cancellationToken);

                var response = updatedInvite.ToRsvpResponse();
                return TypedResults.Ok(response);
            })
            .WithName(Name)
            .Produces<SubmitRsvpResponse>(StatusCodes.Status200OK)
            .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization(AuthConstants.AdminPolicyName)
            .WithApiVersionSet(ApiVersioning.ApiVersionSet!)
            .HasApiVersion(1.0);

        return app;
    }
}
