namespace WeddingRsvp.Api;

public static class AssemblyInstaller
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddOpenApi();

        return services;
    }

    public static WebApplication AddMiddleware(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        // TODO: Map API endpoints.

        return app;
    }
}
