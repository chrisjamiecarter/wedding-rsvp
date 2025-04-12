using WeddingRsvp.Application.Entities;
using WeddingRsvp.Application.Shared;

namespace WeddingRsvp.Application.Services;

public interface IAuthService
{
    Task<Result> ExternalLoginAsync();
    Task<ApplicationUser?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<bool> IsAdminAsync(ApplicationUser? user, CancellationToken cancellationToken = default);
    Task LogoutAsync(CancellationToken cancellationToken = default);
}