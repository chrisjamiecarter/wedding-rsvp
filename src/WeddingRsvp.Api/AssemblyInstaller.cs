using Asp.Versioning;
using WeddingRsvp.Api.Endpoints;
using WeddingRsvp.Api.Endpoints.V1;
using WeddingRsvp.Api.Middlewares;

namespace WeddingRsvp.Api;

public static class AssemblyInstaller
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1.0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = new MediaTypeApiVersionReader("api-version");
        }).AddApiExplorer();

        services.AddEndpointsApiExplorer();
    
        services.AddOpenApi();

        return services;
    }

    public static WebApplication AddApiMiddleware(this WebApplication app)
    {
        app.CreateApiVersionSet();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseMiddleware<ValidationMappingMiddleware>();

        app.MapApiV1Endpoints();

        return app;
    }
}
