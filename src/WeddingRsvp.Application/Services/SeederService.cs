using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WeddingRsvp.Application.Auth;
using WeddingRsvp.Application.Entities;
using WeddingRsvp.Application.Models;
using WeddingRsvp.Application.Options;

namespace WeddingRsvp.Application.Services;

internal sealed class SeederService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SeederOptions _options;
    private readonly ILogger<SeederService> _logger;

    public SeederService(UserManager<ApplicationUser> userManager, ILogger<SeederService> logger, IOptions<SeederOptions> options)
    {
        _userManager = userManager;
        _logger = logger;
        _options = options.Value;
    }

    public async Task SeedDatabaseAsync()
    {
        if (!_options.SeedDatabase)
        {
            return;
        }

        if (_options.SeedUser != null)
        {
            await SeedUserAsync(_options.SeedUser);
        }
    }

    private async Task SeedUserAsync(SeedUser seedUser)
    {
        var user = await _userManager.FindByEmailAsync(seedUser.Email);
        if (user is null)
        {
            user = new ApplicationUser
            {
                UserName = seedUser.Email,
                Email = seedUser.Email,
            };

            var password = GeneratePassword();

            var createdResult = await _userManager.CreateAsync(user, password);
            if (!createdResult.Succeeded)
            {
                foreach (var error in createdResult.Errors)
                {
                    _logger.LogError("Error: {code} - {description}", error.Code, error.Description);
                }

                return;
            }

            _logger.LogInformation("Created seed user: {email} {password}", user.Email, password);

            var claim = new Claim(AuthConstants.AdminClaimName, "true");

            var claimResult = await _userManager.AddClaimAsync(user, claim);
            if (!claimResult.Succeeded)
            {
                foreach (var error in createdResult.Errors)
                {
                    _logger.LogError("Error: {code} - {description}", error.Code, error.Description);
                }

                return;
            }

            _logger.LogInformation("Added claim: {type} {value}", claim.Type, claim.Value);
        }
    }

    private static string GeneratePassword()
    {
        return RandomNumberGenerator.GetString("abcdefghjkmnpqrstuvwxyzABCDEFGHJKMNPQRSTUVWXYZ23456789*@#+%".AsSpan(), 16);
    }
}
