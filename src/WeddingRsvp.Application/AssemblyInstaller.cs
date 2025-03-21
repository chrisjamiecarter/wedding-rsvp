using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeddingRsvp.Application.Auth;
using WeddingRsvp.Application.Cache;
using WeddingRsvp.Application.Database;
using WeddingRsvp.Application.Database.Constants;
using WeddingRsvp.Application.Entities;
using WeddingRsvp.Application.Options;
using WeddingRsvp.Application.Services;

namespace WeddingRsvp.Application;

public static class AssemblyInstaller
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("WeddingRsvpConnection") ?? throw new InvalidOperationException("Connection string 'WeddingRsvpConnection' not found");

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString, npgsqlOptions =>
            {
                npgsqlOptions.UseAdminDatabase("postgres");
                npgsqlOptions.MigrationsHistoryTable(TableConstants.EntityFrameworkCoreMigration, SchemaConstants.EntityFrameworkCore);
            });
        });

        services.AddAuthentication(IdentityConstants.ApplicationScheme)
                .AddBearerToken(IdentityConstants.BearerScheme)
                .AddGoogle(googleOptions =>
                {
                    googleOptions.ClientId = configuration["Authentication:Google:ClientId"] ?? throw new InvalidOperationException("Configuration setting 'Authentication:Google:ClientId' not found");
                    googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"] ?? throw new InvalidOperationException("Configuration setting 'Authentication:Google:ClientSecret' not found");
                    googleOptions.AccessDeniedPath = "/Account/AccessDenied";
                    googleOptions.CallbackPath = "/signin-google";
                })
                .AddIdentityCookies()
                .ApplicationCookie!.Configure(options =>
                {
                    options.Events = new CookieAuthenticationEvents
                    {
                        OnRedirectToLogin = context =>
                        {
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            return Task.CompletedTask;
                        }
                    };
                });

        services.AddAuthorizationBuilder()
                .AddPolicy(AuthConstants.AdminPolicyName, policy =>
                {
                    policy.RequireClaim(AuthConstants.AdminClaimName, "true");
                    policy.AddAuthenticationSchemes(IdentityConstants.ApplicationScheme, IdentityConstants.BearerScheme);
                });
        
        services.AddIdentityCore<ApplicationUser>(options => options.User.RequireUniqueEmail = AuthConstants.UserRequireUniqueEmail)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddApiEndpoints();

        services.AddOutputCache(options =>
        {
            options.AddBasePolicy(policy =>
            {
                policy.Cache();
            });
            options.AddPolicy(Policies.Event.Name, policy =>
            {
                policy.Cache()
                      .Expire(Policies.Event.Expiration)
                      .Tag(Policies.Event.Tag);
            });
            options.AddPolicy(Policies.FoodOption.Name, policy =>
            {
                policy.Cache()
                      .Expire(Policies.FoodOption.Expiration)
                      .Tag(Policies.FoodOption.Tag);
            });
        });

        services.AddScoped<IEventService, EventService>();
        services.AddScoped<IFoodOptionService, FoodOptionService>();
        services.AddScoped<IEventFoodOptionService, EventFoodOptionService>();
        services.AddScoped<IInviteService, InviteService>();
        services.AddScoped<IGuestService, GuestService>();

        services.Configure<SeederOptions>(configuration.GetSection(nameof(SeederOptions)));
        services.AddScoped<SeederService>();

        services.AddValidatorsFromAssembly(AssemblyReference.Assembly, ServiceLifetime.Transient);

        return services;
    }

    public static async Task<WebApplication> AddApplicationMiddlewareAsync(this WebApplication app)
    {
        app.MapGroup("api/auth")
           .MapIdentityApi<ApplicationUser>();

        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        await services.SeedDatabaseAsync();

        return app;
    }

    public static async Task<IServiceProvider> SeedDatabaseAsync(this IServiceProvider serviceProvider)
    {
        var seeder = serviceProvider.GetRequiredService<SeederService>();
        await seeder.SeedDatabaseAsync();

        return serviceProvider;
    }
}
