using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WeddingRsvp.Application.Database;
using WeddingRsvp.Application.Entities;
using WeddingRsvp.Application.Enums;
using WeddingRsvp.Application.Models;

namespace WeddingRsvp.Application.Services;

internal class EventFoodOptionService : IEventFoodOptionService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IValidator<EventFoodOption> _eventFoodOptionValidator;
    private readonly IValidator<GetAllEventFoodOptionsOptions> _getAllEventFoodOptionsOptionsValidator;

    public EventFoodOptionService(ApplicationDbContext dbContext, IValidator<EventFoodOption> eventFoodOptionValidator, IValidator<GetAllEventFoodOptionsOptions> getAllEventFoodOptionsOptionsValidator)
    {
        _dbContext = dbContext;
        _eventFoodOptionValidator = eventFoodOptionValidator;
        _getAllEventFoodOptionsOptionsValidator = getAllEventFoodOptionsOptionsValidator;
    }

    public async Task<bool> CreateAsync(EventFoodOption eventfoodOption, CancellationToken cancellationToken = default)
    {
        await _eventFoodOptionValidator.ValidateAndThrowAsync(eventfoodOption, cancellationToken);

        await _dbContext.EventFoodOptions.AddAsync(eventfoodOption, cancellationToken);

        var result = await _dbContext.SaveChangesAsync(cancellationToken);

        return result > 0;
    }

    public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var existingEventFoodOption = await _dbContext.EventFoodOptions.FindAsync([id], cancellationToken);
        if (existingEventFoodOption is null)
        {
            return false;
        }

        _dbContext.EventFoodOptions.Remove(existingEventFoodOption);

        var result = await _dbContext.SaveChangesAsync(cancellationToken);

        return result > 0;
    }

    public async Task<PaginatedList<EventFoodOption>> GetAllAsync(GetAllEventFoodOptionsOptions options, CancellationToken cancellationToken = default)
    {
        await _getAllEventFoodOptionsOptionsValidator.ValidateAndThrowAsync(options, cancellationToken);

        var query = _dbContext.EventFoodOptions.Include(o => o.FoodOption).AsQueryable();

        query = query.Where(o => o.EventId == options.EventId);

        if (options.FoodType != null)
        {
            query = query.Where(o => o.FoodOption!.FoodType == options.FoodType);
        }

        if (!string.IsNullOrWhiteSpace(options.SortField))
        {
            query = options.SortField switch
            {
                "foodtype" => options.SortOrder == SortOrder.Ascending ? query.OrderBy(o => o.FoodOption!.FoodType).ThenBy(o => o.Id) : query.OrderByDescending(o => o.FoodOption!.FoodType).ThenByDescending(o => o.Id),
                _ => options.SortOrder == SortOrder.Ascending ? query.OrderBy(o => o.Id) : query.OrderByDescending(o => o.Id),
            };
        }

        var count = await query.CountAsync(cancellationToken);
        var items = await query.Skip((options.PageNumber - 1) * options.PageSize).Take(options.PageSize).ToListAsync(cancellationToken);

        return PaginatedList<EventFoodOption>.Create(items, count, options.PageNumber, options.PageSize);
    }

    public Task<EventFoodOption?> GetByEventIdAndFoodOptionIdAsync(Guid eventId, Guid foodOptionId, CancellationToken cancellationToken = default)
    {
        return _dbContext.EventFoodOptions.SingleOrDefaultAsync(p => p.EventId == eventId && p.FoodOptionId == foodOptionId, cancellationToken);
    }

    public Task<EventFoodOption?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _dbContext.EventFoodOptions.SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
    }
}
