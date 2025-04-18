namespace WeddingRsvp.Api.Endpoints.V1.Invites;

public static class InvitesEndpointsExtensions
{
    public static IEndpointRouteBuilder MapInvitesEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapCreateInvite();
        app.MapDeleteInvite();
        app.MapGenerateTokenForInvite();
        app.MapGetAllInvites();
        app.MapGetInvite();
        app.MapUpdateInvite();

        return app;
    }
}
