using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using WeddingRsvp.Application.Auth;
using WeddingRsvp.Application.Entities;
using WeddingRsvp.Application.Shared;

namespace WeddingRsvp.Application.Services;

internal class AuthService : IAuthService
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<Result> ExternalLoginAsync()
    {
        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info is null)
        {
            return Result.Failure(new("ExternalLogin.Info", "External login information is null."));
        }

        var userLogin = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
        if (userLogin is null)
        {
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            if (email is null)
            {
                return Result.Failure(new("ExternalLogin.Email", "Email claim is null."));
            }
            
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                return Result.Failure(new("ExternalLogin.EmailNotFound", "Registering via external provider is not currently supported."));
            }

            var adddLoginResult = await _userManager.AddLoginAsync(user, info);
            if (!adddLoginResult.Succeeded)
            {
                return Result.Failure(new("ExternalLogin.AddLogin", "Failed to add external login."));
            }
        }

        var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
        return signInResult.Succeeded
            ? Result.Success()
            : Result.Failure(new("ExternalLogin.SignIn", "Failed to sign in with external login."));
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
