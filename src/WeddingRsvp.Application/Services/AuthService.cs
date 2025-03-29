using Microsoft.AspNetCore.Identity;
using WeddingRsvp.Application.Auth;
using WeddingRsvp.Application.Entities;

namespace WeddingRsvp.Application.Services;

internal class AuthService : IAuthService
{
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AuthService(SignInManager<ApplicationUser> signInManager)
    {
        _signInManager = signInManager;
    }

    public async Task<ApplicationUser?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _signInManager.UserManager.FindByIdAsync(id);
    }

    public async Task<bool> IsAdminAsync(ApplicationUser? user, CancellationToken cancellationToken = default)
    {
        if (user is null)
        {
            return false;
        }    
        
        var claims = await _signInManager.UserManager.GetClaimsAsync(user);
        return claims.Any(claim => claim.Type == AuthConstants.AdminClaimName && claim.Value == "true");
    }

    public async Task LogoutAsync(CancellationToken cancellationToken = default)
    {
        await _signInManager.SignOutAsync();
    }
}
