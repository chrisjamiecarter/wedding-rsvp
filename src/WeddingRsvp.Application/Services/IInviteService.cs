using WeddingRsvp.Application.Entities;
using WeddingRsvp.Application.Models;

namespace WeddingRsvp.Application.Services;
public interface IInviteService
{
    Task<bool> CreateAsync(Invite invite, CancellationToken cancellationToken = default);
    Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Invite?> GenerateTokenAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Invite>> GetByEventIdAsync(Guid eventId, CancellationToken cancellationToken = default);
    Task<Invite?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Invite?> SubmitRsvp(InviteRsvp inviteRsvp, CancellationToken cancellationToken = default);
    Task<Invite?> UpdateAsync(Invite invite, CancellationToken cancellationToken = default);
}