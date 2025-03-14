using Asp.Versioning.Builder;
using Asp.Versioning.Conventions;

namespace WeddingRsvp.Api.Endpoints;

public static class ApiVersioning
{
    public static ApiVersionSet? ApiVersionSet { get; private set; }

    public static IEndpointRouteBuilder CreateApiVersionSet(this IEndpointRouteBuilder app)
    {
        ApiVersionSet = app.NewApiVersionSet()
                           .HasApiVersion(1.0)
                           .ReportApiVersions()
                           .Build();

        return app;
    }
}
