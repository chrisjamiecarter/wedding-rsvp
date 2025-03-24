using Microsoft.AspNetCore.Identity;
using WeddingRsvp.Application.Entities;

namespace WeddingRsvp.Application.Services;

internal class AuthService : IAuthService
{
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AuthService(SignInManager<ApplicationUser> signInManager)
    {
        _signInManager = signInManager;
    }

    public async Task LogoutAsync(CancellationToken cancellationToken)
    {
        await _signInManager.SignOutAsync();
    }
}
