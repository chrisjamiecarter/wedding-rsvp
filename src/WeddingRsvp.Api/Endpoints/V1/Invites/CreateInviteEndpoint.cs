using Microsoft.AspNetCore.OutputCaching;
using WeddingRsvp.Api.Mappings.V1;
using WeddingRsvp.Application.Auth;
using WeddingRsvp.Application.Cache;
using WeddingRsvp.Application.Services;
using WeddingRsvp.Contracts.Requests.V1.Invites;
using WeddingRsvp.Contracts.Responses;
using WeddingRsvp.Contracts.Responses.V1.Invites;

namespace WeddingRsvp.Api.Endpoints.V1.Invites;

public static class CreateInviteEndpoint
{
    public const string Name = "CreateInvite";

    public static IEndpointRouteBuilder MapCreateInvite(this IEndpointRouteBuilder app)
    {
        app.MapPost(Routes.Events.CreateInvite,
            async (Guid eventId,
                   CreateInviteRequest request,
                   IInviteService inviteService,
                   IOutputCacheStore outputCacheStore,
                   CancellationToken cancellationToken) =>
            {
                var invite = request.ToEntity(eventId);

                await inviteService.CreateAsync(invite, cancellationToken);

                await outputCacheStore.EvictByTagAsync(Policies.Invite.Tag, cancellationToken);

                var response = invite.ToResponse();
                return TypedResults.CreatedAtRoute(response, GetInviteEndpoint.Name, new { id = invite.Id });
            })
            .WithName(Name)
            .Produces<InviteResponse>(StatusCodes.Status201Created)
            .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .RequireAuthorization(AuthConstants.AdminPolicyName)
            .WithApiVersionSet(ApiVersioning.ApiVersionSet!)
            .HasApiVersion(1.0);

        return app;
    }
}
