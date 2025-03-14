using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WeddingRsvp.Application.Auth;
using WeddingRsvp.Application.Entities;
using WeddingRsvp.Application.Extensions;
using WeddingRsvp.Application.Models;
using WeddingRsvp.Application.Options;
using WeddingRsvp.Application.Shared;

namespace WeddingRsvp.Application.Services;

public class AuthService : IAuthService
{
    private readonly JwtOptions _options;
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthService(IOptions<JwtOptions> options, UserManager<ApplicationUser> userManager)
    {
        _options = options.Value;
        _userManager = userManager;
    }

    public async Task<Result<Token>> LoginAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
        {
            return Result.Failure<Token>(new("User.NotFound", $"Unable to find user with email {email}"));
        }

        if (!await _userManager.CheckPasswordAsync(user, password))
        {
            return Result.Failure<Token>(new("User.InvalidSignInAttempt", $"Invalid sign in attempt for user with {email}"));
        }

        var token = await GenerateJwtTokenAsync(user);

        return token;
    }

    public async Task<Result> RegisterAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
        };

        var result = await _userManager.CreateAsync(user, password);

        return result.ToDomainResult();
    }

    private async Task<Token> GenerateJwtTokenAsync(ApplicationUser user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_options.Secret);

        var claims = await _userManager.GetClaimsAsync(user);
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.UserName!));
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email!));
        claims.Add(new Claim(AuthConstants.UserIdClaimName, user.Id));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(_options.TokenLifetimeInHours),
            Issuer = _options.Issuer,
            Audience = _options.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var value = tokenHandler.WriteToken(token);

        return new Token(value);
    }
}
