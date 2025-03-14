using WeddingRsvp.Api.Mappings.V1;
using WeddingRsvp.Application.Services;
using WeddingRsvp.Contracts.Requests.V1;

namespace WeddingRsvp.Api.Endpoints.V1.Auth;

public static class LoginEndpoint
{
    public const string Name = "Login";

    public static IEndpointRouteBuilder MapLogin(this IEndpointRouteBuilder app)
    {
        app.MapPost(Routes.Auth.Login,
            async (LoginRequest request,
                   IAuthService authService,
                   CancellationToken cancellationToken) =>
            {
                var result = await authService.LoginAsync(request.Email, request.Password, cancellationToken);
                if (result.IsFailure)
                {
                    return Results.BadRequest("Invalid login attempt");
                }

                var response = result.Value.ToResponse();
                return Results.Ok(response);
            })
            .WithName(Name)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithApiVersionSet(ApiVersioning.ApiVersionSet!)
            .HasApiVersion(1.0);

        return app;
    }
}
