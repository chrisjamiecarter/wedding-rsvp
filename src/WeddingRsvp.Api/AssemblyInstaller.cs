using Asp.Versioning;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using WeddingRsvp.Api.Endpoints;
using WeddingRsvp.Api.Endpoints.V1;
using WeddingRsvp.Api.Middlewares;
using WeddingRsvp.Api.OpenApi;
using WeddingRsvp.Application.Cors;

namespace WeddingRsvp.Api;

public static class AssemblyInstaller
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(Policies.Web.Name, policy =>
            {
                policy.WithOrigins(Policies.Web.Origins)
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials();
            });
        });

        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1.0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = new MediaTypeApiVersionReader("api-version");
        }).AddApiExplorer();

        services.AddEndpointsApiExplorer();

        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        services.AddSwaggerGen(options =>
        {
            options.OperationFilter<SwaggerDefaultValues>();
        });

        services.AddOpenApi();

        return services;
    }

    public static WebApplication AddApiMiddleware(this WebApplication app)
    {
        app.CreateApiVersionSet();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var description in app.DescribeApiVersions())
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName);
                }
            });
        }

        app.UseCors(Policies.Web.Name);

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseMiddleware<ValidationMappingMiddleware>();

        app.MapApiV1Endpoints();

        return app;
    }
}
