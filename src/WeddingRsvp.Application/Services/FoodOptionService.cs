using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WeddingRsvp.Application.Database;
using WeddingRsvp.Application.Entities;
using WeddingRsvp.Application.Enums;
using WeddingRsvp.Application.Models;

namespace WeddingRsvp.Application.Services;

internal class FoodOptionService : IFoodOptionService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IValidator<FoodOption> _foodOptionValidator;
    private readonly IValidator<GetAllFoodOptionsOptions> _getAllFoodOptionsOptionsValidator;

    public FoodOptionService(ApplicationDbContext dbContext, IValidator<FoodOption> foodOptionValidator, IValidator<GetAllFoodOptionsOptions> getAllFoodOptionsOptionsValidator)
    {
        _dbContext = dbContext;
        _foodOptionValidator = foodOptionValidator;
        _getAllFoodOptionsOptionsValidator = getAllFoodOptionsOptionsValidator;
    }

    public async Task<bool> CreateAsync(FoodOption foodOption, CancellationToken cancellationToken = default)
    {
        await _foodOptionValidator.ValidateAndThrowAsync(foodOption, cancellationToken);

        await _dbContext.FoodOptions.AddAsync(foodOption, cancellationToken);

        var result = await _dbContext.SaveChangesAsync(cancellationToken);

        return result > 0;
    }

    public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var existingFoodOption = await _dbContext.FoodOptions.FindAsync([id], cancellationToken);
        if (existingFoodOption is null)
        {
            return false;
        }

        _dbContext.FoodOptions.Remove(existingFoodOption);

        var result = await _dbContext.SaveChangesAsync(cancellationToken);

        return result > 0;
    }

    public async Task<PaginatedList<FoodOption>> GetAllAsync(GetAllFoodOptionsOptions options, CancellationToken cancellationToken = default)
    {
        await _getAllFoodOptionsOptionsValidator.ValidateAndThrowAsync(options, cancellationToken);

        var query = _dbContext.FoodOptions.AsQueryable();

        query = query.Where(o => o.EventId == options.EventId);

        if (!string.IsNullOrWhiteSpace(options.Name))
        {
            query = query.Where(o => o.Name.Contains(options.Name));
        }

        if (options.FoodType != null)
        {
            query = query.Where(o => o.FoodType == options.FoodType);
        }

        if (!string.IsNullOrWhiteSpace(options.SortField))
        {
            query = options.SortField switch
            {
                "name" => options.SortOrder == SortOrder.Ascending ? query.OrderBy(o => o.Name).ThenBy(o => o.Id) : query.OrderByDescending(o => o.Name).ThenByDescending(o => o.Id),
                "foodtype" => options.SortOrder == SortOrder.Ascending ? query.OrderBy(o => o.FoodType).ThenBy(o => o.Id) : query.OrderByDescending(o => o.FoodType).ThenByDescending(o => o.Id),
                _ => options.SortOrder == SortOrder.Ascending ? query.OrderBy(o => o.Id) : query.OrderByDescending(o => o.Id),
            };
        }

        var count = await query.CountAsync(cancellationToken);
        var items = await query.Skip((options.PageNumber - 1) * options.PageSize).Take(options.PageSize).ToListAsync(cancellationToken);

        return PaginatedList<FoodOption>.Create(items, count, options.PageNumber, options.PageSize);
    }

    public async Task<FoodOption?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.FoodOptions.SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<FoodOption?> UpdateAsync(FoodOption foodOption, CancellationToken cancellationToken = default)
    {
        await _foodOptionValidator.ValidateAndThrowAsync(foodOption, cancellationToken);

        var existingFoodOption = await _dbContext.FoodOptions.FindAsync([foodOption.Id], cancellationToken);
        if (existingFoodOption is null)
        {
            return null;
        }

        _dbContext.FoodOptions.Entry(existingFoodOption).CurrentValues.SetValues(foodOption);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return foodOption;
    }
}
