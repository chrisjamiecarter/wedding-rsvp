using WeddingRsvp.Application;

namespace WeddingRsvp.Api;

internal static class Program
{
    internal static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddApplicationServices(builder.Configuration);

        builder.Services.AddApiServices();

        var app = builder.Build();

        app.AddApplicationMiddleware();

        app.AddApiMiddleware();
                
        await app.RunAsync();
    }
}
