using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using WeddingRsvp.Application.Auth;
using WeddingRsvp.Application.Database;
using WeddingRsvp.Application.Entities;
using WeddingRsvp.Application.Options;

namespace WeddingRsvp.Application;

public static class AssemblyInstaller
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("WeddingRsvpConnection") ?? throw new InvalidOperationException("Connection string 'WeddingRsvpConnection' not found");

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString, npgsqlOptions =>
            {
                npgsqlOptions.UseAdminDatabase("postgres");
            });
        });

        services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

        JwtOptions jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>() ?? throw new InvalidOperationException("Configuration section 'JwtOptions' not found");

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
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

        services.AddAuthorizationBuilder()
        .AddPolicy(AuthConstants.AdminPolicyName, policy =>
        {
            policy.RequireRole(AuthConstants.AdminRoleName);
        });

        return services;
    }
}
