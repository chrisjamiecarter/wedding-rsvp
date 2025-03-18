using WeddingRsvp.Api.Mappings.V1;
using WeddingRsvp.Application.Auth;
using WeddingRsvp.Application.Cache;
using WeddingRsvp.Application.Services;
using WeddingRsvp.Contracts.Responses.V1.Invites;

namespace WeddingRsvp.Api.Endpoints.V1.Invites;

public static class GetInviteEndpoint
{
    public const string Name = "GetInvite";

    public static IEndpointRouteBuilder MapGetInvite(this IEndpointRouteBuilder app)
    {
        app.MapGet(Routes.Invites.Get,
            async (Guid id,
                   IInviteService inviteService,
                   CancellationToken cancellationToken) =>
            {
                var invite = await inviteService.GetByIdAsync(id, cancellationToken);

                return invite != null
                    ? TypedResults.Ok(invite.ToResponse())
                    : Results.NotFound();
            })
            .WithName(Name)
            .Produces<InviteResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization(AuthConstants.AdminPolicyName)
            .WithApiVersionSet(ApiVersioning.ApiVersionSet!)
            .HasApiVersion(1.0)
            .CacheOutput(Policies.Invite.Name);

        return app;
    }
}
