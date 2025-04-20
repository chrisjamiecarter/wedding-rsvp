using WeddingRsvp.Application.Entities;
using WeddingRsvp.Application.Enums;
using WeddingRsvp.Application.Models;
using WeddingRsvp.Application.Shared;

namespace WeddingRsvp.Application.Services;

public interface IRsvpService
{
    Task<Invite?> GetAsync(Guid inviteId, Guid token, CancellationToken cancellationToken = default);
    Task<IEnumerable<FoodOption>> GetFoodOptionsAsync(Guid inviteId, Guid token, FoodType foodType, CancellationToken cancellationToken = default);
    Task<IEnumerable<Guest>> GetGuestsAsync(Guid inviteId, Guid token, CancellationToken cancellationToken = default);
    Task<Result> SubmitAsync(InviteRsvp inviteRsvp, CancellationToken cancellationToken = default);
}