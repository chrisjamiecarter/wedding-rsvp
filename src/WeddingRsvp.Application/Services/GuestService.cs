using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WeddingRsvp.Application.Database;
using WeddingRsvp.Application.Entities;
using WeddingRsvp.Application.Enums;
using WeddingRsvp.Application.Models;

namespace WeddingRsvp.Application.Services;

internal class GuestService : IGuestService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IValidator<Guest> _guestValidator;
    private readonly IValidator<GetAllGuestsForEventOptions> _getAllGuestsForEventOptionsValidator;
    private readonly IValidator<GetAllGuestsForInviteOptions> _GetAllGuestsForInviteOptionsValidator;

    public GuestService(ApplicationDbContext dbContext,
                         IValidator<Guest> guestValidator,
                         IValidator<GetAllGuestsForEventOptions> getAllGuestsForEventOptionsValidator,
                         IValidator<GetAllGuestsForInviteOptions> getAllGuestsForInviteOptionsValidator)
    {
        _dbContext = dbContext;
        _guestValidator = guestValidator;
        _getAllGuestsForEventOptionsValidator = getAllGuestsForEventOptionsValidator;
        _GetAllGuestsForInviteOptionsValidator = getAllGuestsForInviteOptionsValidator;
    }

    public async Task<bool> CreateAsync(Guest guest, CancellationToken cancellationToken = default)
    {
        await _guestValidator.ValidateAsync(guest, options =>
        {
            options.IncludeRuleSets("Create", "default");
            options.ThrowOnFailures();
        },
        cancellationToken);

        await _dbContext.Guests.AddAsync(guest, cancellationToken);

        var result = await _dbContext.SaveChangesAsync(cancellationToken);

        return result > 0;
    }

    public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var guest = await _dbContext.Guests.FindAsync([id], cancellationToken);
        if (guest is null)
        {
            return false;
        }

        _dbContext.Guests.Remove(guest);

        var result = await _dbContext.SaveChangesAsync(cancellationToken);

        return result > 0;
    }

    public async Task<PaginatedList<Guest>> GetAllGuestsForEventOptionsAsync(GetAllGuestsForEventOptions options, CancellationToken cancellationToken = default)
    {
        await _getAllGuestsForEventOptionsValidator.ValidateAndThrowAsync(options, cancellationToken);

        var query = _dbContext.Guests.Include(e => e.Invite).Include(e => e.MainFoodOption).Include(e => e.DessertFoodOption).AsQueryable();

        query = query.Where(g => g.Invite!.EventId == options.EventId);

        if (!string.IsNullOrWhiteSpace(options.Name))
        {
            query = query.Where(g => g.Name.Contains(options.Name));
        }

        if (options.RsvpStatus != null)
        {
            query = query.Where(g => g.RsvpStatus == options.RsvpStatus);
        }

        if (!string.IsNullOrWhiteSpace(options.HouseholdName))
        {
            query = query.Where(g => g.Invite!.HouseholdName.Contains(options.HouseholdName));
        }

        if (!string.IsNullOrWhiteSpace(options.SortField))
        {
            query = options.SortField switch
            {
                "name" => options.SortOrder == SortOrder.Ascending ? query.OrderBy(g => g.Name).ThenBy(g => g.Id) : query.OrderByDescending(g => g.Name).ThenByDescending(g => g.Id),
                "rvsp" => options.SortOrder == SortOrder.Ascending ? query.OrderBy(g => g.RsvpStatus).ThenBy(g => g.Id) : query.OrderByDescending(g => g.RsvpStatus).ThenByDescending(g => g.Id),
                "household" => options.SortOrder == SortOrder.Ascending ? query.OrderBy(g => g.Invite!.HouseholdName).ThenBy(g => g.Id) : query.OrderByDescending(g => g.Invite!.HouseholdName).ThenByDescending(g => g.Id),
                "main" => options.SortOrder == SortOrder.Ascending ? query.OrderBy(g => g.MainFoodOption!.Name).ThenBy(g => g.Id) : query.OrderByDescending(g => g.MainFoodOption!.Name).ThenByDescending(g => g.Id),
                "dessert" => options.SortOrder == SortOrder.Ascending ? query.OrderBy(g => g.DessertFoodOption!.Name).ThenBy(g => g.Id) : query.OrderByDescending(g => g.DessertFoodOption!.Name).ThenByDescending(g => g.Id),
                _ => options.SortOrder == SortOrder.Ascending ? query.OrderBy(g => g.Id) : query.OrderByDescending(g => g.Id),
            };
        }

        var count = await query.CountAsync(cancellationToken);
        var items = await query.Skip((options.PageNumber - 1) * options.PageSize).Take(options.PageSize).ToListAsync(cancellationToken);

        return PaginatedList<Guest>.Create(items, count, options.PageNumber, options.PageSize);
    }

    public async Task<PaginatedList<Guest>> GetAllGuestsForInviteOptionsAsync(GetAllGuestsForInviteOptions options, CancellationToken cancellationToken = default)
    {
        await _GetAllGuestsForInviteOptionsValidator.ValidateAndThrowAsync(options, cancellationToken);

        var query = _dbContext.Guests.Include(e => e.Invite).Include(e => e.MainFoodOption).Include(e => e.DessertFoodOption).AsQueryable();

        query = query.Where(g => g.InviteId == options.InviteId);

        if (!string.IsNullOrWhiteSpace(options.Name))
        {
            query = query.Where(g => g.Name.Contains(options.Name));
        }

        if (options.RsvpStatus != null)
        {
            query = query.Where(g => g.RsvpStatus == options.RsvpStatus);
        }

        if (!string.IsNullOrWhiteSpace(options.SortField))
        {
            query = options.SortField switch
            {
                "name" => options.SortOrder == SortOrder.Ascending ? query.OrderBy(g => g.Name).ThenBy(g => g.Id) : query.OrderByDescending(g => g.Name).ThenByDescending(g => g.Id),
                "rvsp" => options.SortOrder == SortOrder.Ascending ? query.OrderBy(g => g.RsvpStatus).ThenBy(g => g.Id) : query.OrderByDescending(g => g.RsvpStatus).ThenByDescending(g => g.Id),
                "main" => options.SortOrder == SortOrder.Ascending ? query.OrderBy(g => g.MainFoodOption!.Name).ThenBy(g => g.Id) : query.OrderByDescending(g => g.MainFoodOption!.Name).ThenByDescending(g => g.Id),
                "dessert" => options.SortOrder == SortOrder.Ascending ? query.OrderBy(g => g.DessertFoodOption!.Name).ThenBy(g => g.Id) : query.OrderByDescending(g => g.DessertFoodOption!.Name).ThenByDescending(g => g.Id),
                _ => options.SortOrder == SortOrder.Ascending ? query.OrderBy(g => g.Id) : query.OrderByDescending(g => g.Id),
            };
        }

        var count = await query.CountAsync(cancellationToken);
        var items = await query.Skip((options.PageNumber - 1) * options.PageSize).Take(options.PageSize).ToListAsync(cancellationToken);

        return PaginatedList<Guest>.Create(items, count, options.PageNumber, options.PageSize);
    }

    public async Task<Guest?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Guests.SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<Guest?> UpdateAsync(Guest guest, CancellationToken cancellationToken = default)
    {
        await _guestValidator.ValidateAsync(guest, options =>
        {
            options.IncludeRuleSets("Update", "default");
            options.ThrowOnFailures();
        },
        cancellationToken);

        var existingGuest = await _dbContext.Guests.FindAsync([guest.Id], cancellationToken);
        if (existingGuest is null)
        {
            return null;
        }

        _dbContext.Guests.Entry(existingGuest).CurrentValues.SetValues(guest);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return guest;
    }
}
