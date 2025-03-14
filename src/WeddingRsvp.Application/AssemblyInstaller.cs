using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using WeddingRsvp.Application.Auth;
using WeddingRsvp.Application.Database;
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
            });
            options.UseSeeding((context, _) =>
            {
                var email = "chrisjamiecarter@gmail.com";
                var user = context.Set<ApplicationUser>().SingleOrDefault(x => x.Email == email);
                if (user is null)
                {
                    user = new ApplicationUser
                    {
                        UserName = email,
                        Email = email,
                    };

                    context.Set<ApplicationUser>().Add(user);
                    context.SaveChanges();
                }
            });
        });

        services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                //.AddSignInManager()
                .AddDefaultTokenProviders();

        JwtOptions jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>() ?? throw new InvalidOperationException("Configuration section 'JwtOptions' not found");
        services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(IdentityConstants.BearerScheme, options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret)),
                ValidAudience = jwtOptions.Audience,
                ValidateAudience = true,
                ValidIssuer = jwtOptions.Issuer,
                ValidateIssuer = true,
                ValidateLifetime = true,
            };
        })
        .AddGoogle(googleOptions =>
        {
            googleOptions.ClientId = configuration["Authentication:Google:ClientId"] ?? throw new InvalidOperationException("Configuration setting 'Authentication:Google:ClientId' not found");
            googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"] ?? throw new InvalidOperationException("Configuration setting 'Authentication:Google:ClientSecret' not found");
            //googleOptions.AccessDeniedPath = "/Account/AccessDenied";
            googleOptions.CallbackPath = "/signin-google";
        });
        //.AddIdentityCookies();

        services.AddAuthorizationBuilder()
        .AddPolicy(AuthConstants.AdminPolicyName, policy =>
        {
            policy.RequireClaim(AuthConstants.AdminClaimName, "true");
        });

        services.AddScoped<IAuthService, AuthService>();

        services.Configure<SeederOptions>(configuration.GetSection(nameof(SeederOptions)));
        services.AddScoped<SeederService>();

        return services;
    }

    public static async Task<WebApplication> AddApplicationMiddlewareAsync(this WebApplication app)
    {
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
