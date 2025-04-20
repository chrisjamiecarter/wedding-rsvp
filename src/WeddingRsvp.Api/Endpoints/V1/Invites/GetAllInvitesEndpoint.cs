using WeddingRsvp.Api.Mappings.V1;
using WeddingRsvp.Application.Auth;
using WeddingRsvp.Application.Cache;
using WeddingRsvp.Application.Services;
using WeddingRsvp.Contracts.Requests.V1.Invites;
using WeddingRsvp.Contracts.Responses.V1.Invites;

namespace WeddingRsvp.Api.Endpoints.V1.Invites;

public static class GetAllInvitesEndpoint
{
    public const string Name = "GetInvites";

    public static IEndpointRouteBuilder MapGetAllInvites(this IEndpointRouteBuilder app)
    {
        app.MapGet(Routes.Events.GetAllInvites,
            async (Guid eventId,
                   [AsParameters] GetAllInvitesRequest request,
                   IInviteService inviteService,
                   CancellationToken cancellationToken) =>
            {
                var options = request.ToOptions(eventId);

                var invites = await inviteService.GetAllAsync(options, cancellationToken);

                return TypedResults.Ok(invites.ToResponse());
            })
            .WithName(Name)
            .Produces<InvitesResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .RequireAuthorization(AuthConstants.AdminPolicyName)
            .WithApiVersionSet(ApiVersioning.ApiVersionSet!)
            .HasApiVersion(1.0)
            .CacheOutput(Policies.Invite.Name);

        return app;
    }
}
