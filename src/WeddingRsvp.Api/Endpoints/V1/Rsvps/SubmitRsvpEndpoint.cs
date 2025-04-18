using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using WeddingRsvp.Api.Mappings.V1;
using WeddingRsvp.Application.Cache;
using WeddingRsvp.Application.Services;
using WeddingRsvp.Contracts.Requests.V1.Rsvps;
using WeddingRsvp.Contracts.Responses;

namespace WeddingRsvp.Api.Endpoints.V1.Rsvps;

public static class SubmitRsvpEndpoint
{
    public const string Name = "SubmitRsvp";

    public static IEndpointRouteBuilder MapSubmitRsvp(this IEndpointRouteBuilder app)
    {
        app.MapPost(Routes.Rsvps.Submit,
            async (Guid inviteId,
                   [FromBody] SubmitRsvpRequest request,
                   IRsvpService rsvpService,
                   IOutputCacheStore outputCacheStore,
                   CancellationToken cancellationToken) =>
            {
                var invite = await rsvpService.GetAsync(inviteId, request.Token, cancellationToken);
                if (invite is null)
                {
                    return Results.NotFound();
                }

                var inviteRsvp = request.ToModel(inviteId);

                var result = await rsvpService.SubmitAsync(inviteRsvp, cancellationToken);
                if (result.IsFailure)
                {
                    return Results.BadRequest(result.Error);
                }

                await outputCacheStore.EvictByTagAsync(Policies.Invite.Tag, cancellationToken);
                await outputCacheStore.EvictByTagAsync(Policies.Guest.Tag, cancellationToken);
                await outputCacheStore.EvictByTagAsync(Policies.Rsvp.Tag, cancellationToken);

                return Results.NoContent();
            })
            .WithName(Name)
            .Produces(StatusCodes.Status204NoContent)
            .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithApiVersionSet(ApiVersioning.ApiVersionSet!)
            .HasApiVersion(1.0);

        return app;
    }
}
