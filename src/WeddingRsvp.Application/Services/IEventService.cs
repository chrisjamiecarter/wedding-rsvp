using WeddingRsvp.Application.Entities;
using WeddingRsvp.Application.Models;

namespace WeddingRsvp.Application.Services;

public interface IEventService
{
    Task<bool> CreateAsync(Event evnt, CancellationToken cancellationToken = default);
    Task<PaginatedList<Event>> GetAllAsync(GetAllEventsOptions options, CancellationToken cancellationToken = default);
    Task<Event?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}