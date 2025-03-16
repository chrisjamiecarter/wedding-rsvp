using WeddingRsvp.Application.Entities;
using WeddingRsvp.Application.Models;

namespace WeddingRsvp.Application.Services;

public interface IFoodOptionService
{
    Task<bool> CreateAsync(FoodOption foodOption, CancellationToken cancellationToken = default);
    Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PaginatedList<FoodOption>> GetAllAsync(GetAllFoodOptionsOptions options, CancellationToken cancellationToken = default);
    Task<FoodOption?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<FoodOption?> UpdateAsync(FoodOption foodOption, CancellationToken cancellationToken = default);
}