using Microsoft.AspNetCore.Identity;
using WeddingRsvp.Api.Endpoints.V1;
using WeddingRsvp.Application.Entities;
using WeddingRsvp.Application.Services;
using WeddingRsvp.Contracts.Requests.V1;

namespace WeddingRsvp.Api.Endpoints.V1.Auth;

public static class RegisterEndpoint
{
    public const string Name = "Register";

    public static IEndpointRouteBuilder MapRegister(this IEndpointRouteBuilder app)
    {
        app.MapPost(Routes.Auth.Register,
            async (RegisterRequest request,
                   IAuthService authService,
                   CancellationToken cancellationToken) =>
            {
                var result = await authService.RegisterAsync(request.Email, request.Password, cancellationToken);

                return result.IsSuccess 
                    ? Results.NoContent()
                    : Results.BadRequest(result.Error);
            })
            .WithName(Name)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .WithApiVersionSet(ApiVersioning.ApiVersionSet!)
            .HasApiVersion(1.0);

        return app;
    }
}
