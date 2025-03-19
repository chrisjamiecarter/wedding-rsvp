using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using WeddingRsvp.Application.Auth;
using WeddingRsvp.Application.Database;
using WeddingRsvp.Application.Entities;
using WeddingRsvp.Application.Extensions;
using WeddingRsvp.Application.Models;
using WeddingRsvp.Application.Options;
using WeddingRsvp.Application.Shared;

namespace WeddingRsvp.Application.Services;

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly JwtOptions _options;
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthService(ApplicationDbContext dbContext, IOptions<JwtOptions> options, UserManager<ApplicationUser> userManager)
    {
        _dbContext = dbContext;
        _options = options.Value;
        _userManager = userManager;
    }

    public async Task<Result<AuthToken>> LoginAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
        {
            return Result.Failure<AuthToken>(new("User.NotFound", $"Unable to find user with email {email}"));
        }

        if (!await _userManager.CheckPasswordAsync(user, password))
        {
            return Result.Failure<AuthToken>(new("User.InvalidSignInAttempt", $"Invalid sign in attempt for user with {email}"));
        }

        string accessToken = await GenerateAccessTokenAsync(user);

        var refreshToken = RefreshToken.Create(user.Id);

        await _dbContext.RefreshTokens.AddAsync(refreshToken, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new AuthToken(accessToken, refreshToken);
    }

    public async Task<Result<AuthToken>> RefreshAsync(string refreshTokenValue, CancellationToken cancellationToken = default)
    {
        var refreshToken = await _dbContext.RefreshTokens.Include(r => r.User)
                                                         .SingleOrDefaultAsync(r => r.Value == refreshTokenValue, cancellationToken);
        if (refreshToken == null || refreshToken.ExpiresOnUtc < DateTime.UtcNow)
        {
            return Result.Failure<AuthToken>(new("User.RefreshToken", "The refresh token has expired"));
        }

        string accessToken = await GenerateAccessTokenAsync(refreshToken.User!);

        refreshToken.Refresh();

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new AuthToken(accessToken, refreshToken);
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

    private async Task<string> GenerateAccessTokenAsync(ApplicationUser user)
    {
        string secretKey = _options.Secret;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var claims = await _userManager.GetClaimsAsync(user);
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.UserName!));
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email!));
        claims.Add(new Claim(AuthConstants.UserIdClaimName, user.Id));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            // TODO: Change to minutes.
            Expires = DateTime.UtcNow.AddHours(_options.AccessTokenLifetimeInMinutes),
            Issuer = _options.Issuer,
            Audience = _options.Audience,
            SigningCredentials = credentials,
        };

        var handler = new JsonWebTokenHandler();

        string token = handler.CreateToken(tokenDescriptor);

        return token;
    }
}
