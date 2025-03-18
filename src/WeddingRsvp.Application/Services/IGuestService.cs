using WeddingRsvp.Application.Entities;
using WeddingRsvp.Application.Models;

namespace WeddingRsvp.Application.Services;

public interface IGuestService
{
    Task<bool> CreateAsync(Guest guest, CancellationToken cancellationToken = default);
    Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PaginatedList<Guest>> GetAllGuestsForEventOptionsAsync(GetAllGuestsForEventOptions options, CancellationToken cancellationToken = default);
    Task<PaginatedList<Guest>> GetAllGuestsForInviteOptionsAsync(GetAllGuestsForInviteOptions options, CancellationToken cancellationToken = default);
    Task<Guest?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Guest?> UpdateAsync(Guest guest, CancellationToken cancellationToken = default);
}