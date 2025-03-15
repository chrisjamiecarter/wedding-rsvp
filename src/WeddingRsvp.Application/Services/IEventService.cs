using WeddingRsvp.Application.Entities;
using WeddingRsvp.Application.Models;

namespace WeddingRsvp.Application.Services;

public interface IEventService
{
    Task<PaginatedList<Event>> GetAllAsync(GetAllEventsOptions options, CancellationToken cancellationToken = default);
}