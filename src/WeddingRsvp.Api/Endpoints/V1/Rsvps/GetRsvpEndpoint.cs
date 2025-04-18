using Microsoft.AspNetCore.Mvc;
using WeddingRsvp.Api.Mappings.V1;
using WeddingRsvp.Application.Cache;
using WeddingRsvp.Application.Services;
using WeddingRsvp.Contracts.Requests.V1.Rsvps;
using WeddingRsvp.Contracts.Responses.V1.Rsvps;

namespace WeddingRsvp.Api.Endpoints.V1.Rsvps;

public static class GetRsvpEndpoint
{
    public const string Name = "GetRsvp";

    public static IEndpointRouteBuilder MapGetRsvp(this IEndpointRouteBuilder app)
    {
        app.MapGet(Routes.Rsvps.Get,
            async (Guid inviteId,
                   [AsParameters] GetRsvpRequest request,
                   IRsvpService rsvpService,
                   CancellationToken cancellationToken) =>
            {
                if (request.Token is null)
                {
                    return Results.BadRequest("Token is a required query parameter");
                }

                var invite = await rsvpService.GetAsync(inviteId, request.Token.Value, cancellationToken);
                if (invite is null)
                {
                    return Results.NotFound();
                }

                if (invite.Token is null)
                {
                    return Results.StatusCode(410);
                }

                if (invite.Token != request.Token)
                {
                    return Results.Forbid();
                }

                var guests = await rsvpService.GetGuestsAsync(invite.Id, request.Token.Value, cancellationToken);

                return TypedResults.Ok(invite.ToGetRsvpResponse(guests));
            })
            .WithName(Name)
            .Produces<GetRsvpResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status410Gone)
            .WithApiVersionSet(ApiVersioning.ApiVersionSet!)
            .HasApiVersion(1.0)
            .CacheOutput(Policies.Rsvp.Name);

        return app;
    }
}
