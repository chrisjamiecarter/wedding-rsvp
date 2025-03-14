using WeddingRsvp.Application.Models;
using WeddingRsvp.Application.Shared;

namespace WeddingRsvp.Application.Services;

public interface IAuthService
{
    Task<Result<Token>> LoginAsync(string email, string password, CancellationToken cancellationToken = default);
    Task<Result> RegisterAsync(string email, string password, CancellationToken cancellationToken = default);
}