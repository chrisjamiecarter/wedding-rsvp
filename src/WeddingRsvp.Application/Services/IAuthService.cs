using WeddingRsvp.Application.Models;
using WeddingRsvp.Application.Shared;

namespace WeddingRsvp.Application.Services;

public interface IAuthService
{
    Task<Result<AuthToken>> LoginAsync(string email, string password, CancellationToken cancellationToken = default);
    Task<Result<AuthToken>> RefreshAsync(string refreshTokenValue, CancellationToken cancellationToken = default);
    Task<Result> RegisterAsync(string email, string password, CancellationToken cancellationToken = default);
}