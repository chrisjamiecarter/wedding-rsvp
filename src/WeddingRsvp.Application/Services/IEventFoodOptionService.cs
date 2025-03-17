using WeddingRsvp.Application.Entities;
using WeddingRsvp.Application.Models;

namespace WeddingRsvp.Application.Services;

public interface IEventFoodOptionService
{
    Task<bool> CreateAsync(EventFoodOption eventfoodOption, CancellationToken cancellationToken = default);
    Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PaginatedList<EventFoodOption>> GetAllAsync(GetAllEventFoodOptionsOptions options, CancellationToken cancellationToken = default);
    Task<EventFoodOption?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}