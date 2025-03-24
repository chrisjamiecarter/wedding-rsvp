namespace WeddingRsvp.Application.Services;

public interface IAuthService
{
    Task LogoutAsync(CancellationToken cancellationToken);
}