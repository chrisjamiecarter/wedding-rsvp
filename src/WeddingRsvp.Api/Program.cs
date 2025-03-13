using WeddingRsvp.Application;

namespace WeddingRsvp.Api;

internal static class Program
{
    internal static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddApplication(builder.Configuration);

        builder.Services.AddApi();

        var app = builder.Build();

        app.AddMiddleware();
                
        await app.RunAsync();
    }
}
