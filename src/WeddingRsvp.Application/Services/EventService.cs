using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WeddingRsvp.Application.Database;
using WeddingRsvp.Application.Entities;
using WeddingRsvp.Application.Enums;
using WeddingRsvp.Application.Models;

namespace WeddingRsvp.Application.Services;

internal class EventService : IEventService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IValidator<Event> _eventValidator;
    private readonly IValidator<GetAllEventsOptions> _getAllEventsOptionsValidator;

    public EventService(ApplicationDbContext dbContext, IValidator<Event> eventValidator, IValidator<GetAllEventsOptions> getAllEventsOptionsValidator)
    {
        _dbContext = dbContext;
        _eventValidator = eventValidator;
        _getAllEventsOptionsValidator = getAllEventsOptionsValidator;
    }

    public async Task<bool> CreateAsync(Event evnt, CancellationToken cancellationToken = default)
    {
        await _eventValidator.ValidateAndThrowAsync(evnt, cancellationToken);

        await _dbContext.Events.AddAsync(evnt, cancellationToken);
        
        var result = await _dbContext.SaveChangesAsync(cancellationToken);
        
        return result > 0;
    }

    public async Task<bool> DeleteAsync(DeleteEventOptions options, CancellationToken cancellationToken = default)
    {
        var existingEvent = await _dbContext.Events.SingleOrDefaultAsync(e => e.Id == options.EventId && e.UserId == options.UserId, cancellationToken);
        if (existingEvent is null)
        {
            return false;
        }

        _dbContext.Events.Remove(existingEvent);

        var result = await _dbContext.SaveChangesAsync(cancellationToken);

        return result > 0;
    }

    public async Task<PaginatedList<Event>> GetAllAsync(GetAllEventsOptions options, CancellationToken cancellationToken = default)
    {
        await _getAllEventsOptionsValidator.ValidateAndThrowAsync(options, cancellationToken);

        var query = _dbContext.Events.AsQueryable();

        query = query.Where(e => e.UserId == options.UserId);

        if (!string.IsNullOrWhiteSpace(options.Name))
        {
            query = query.Where(e => e.Name.Contains(options.Name));
        }

        if (!string.IsNullOrWhiteSpace(options.Venue))
        {
            query = query.Where(e => e.Venue.Contains(options.Venue));
        }

        if (options.DateFrom != null)
        {
            query = query.Where(e => e.Date >= options.DateFrom);
        }

        if (options.DateTo != null)
        {
            query = query.Where(e => e.Date <= options.DateTo);
        }

        if (!string.IsNullOrWhiteSpace(options.SortField))
        {
            query = options.SortField switch
            {
                "name" => options.SortOrder == SortOrder.Ascending ? query.OrderBy(e => e.Name) : query.OrderByDescending(e => e.Name),
                "venue" => options.SortOrder == SortOrder.Ascending ? query.OrderBy(e => e.Venue) : query.OrderByDescending(e => e.Venue),
                "date" => options.SortOrder == SortOrder.Ascending ? query.OrderBy(e => e.Date).ThenBy(e => e.Time) : query.OrderByDescending(e => e.Date).ThenByDescending(e => e.Time),
                _ => options.SortOrder == SortOrder.Ascending ? query.OrderBy(e => e.Id) : query.OrderByDescending(e => e.Id),
            };
        }

        var count = await query.CountAsync(cancellationToken);
        var items = await query.Skip((options.PageNumber - 1) * options.PageSize).Take(options.PageSize).ToListAsync(cancellationToken);

        return PaginatedList<Event>.Create(items, count, options.PageNumber, options.PageSize);
    }

    public async Task<Event?> GetAsync(GetEventOptions options, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Events.SingleOrDefaultAsync(e => e.Id == options.EventId && e.UserId == options.UserId, cancellationToken);
    }

    public async Task<Event?> UpdateAsync(Event evnt, CancellationToken cancellationToken = default)
    {
        await _eventValidator.ValidateAndThrowAsync(evnt, cancellationToken);

        var existingEvent = await _dbContext.Events.SingleOrDefaultAsync(e => e.Id == evnt.Id && e.UserId == evnt.UserId, cancellationToken);
        if (existingEvent is null)
        {
            return null;
        }

        _dbContext.Events.Entry(existingEvent).CurrentValues.SetValues(evnt);
        
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return evnt;
    }
}
