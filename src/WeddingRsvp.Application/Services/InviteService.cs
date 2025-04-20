using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WeddingRsvp.Application.Database;
using WeddingRsvp.Application.Entities;
using WeddingRsvp.Application.Enums;
using WeddingRsvp.Application.Models;

namespace WeddingRsvp.Application.Services;

internal class InviteService : IInviteService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IValidator<Invite> _inviteValidator;
    private readonly IValidator<GetAllInvitesOptions> _getAllInvitesOptionsValidator;

    public InviteService(ApplicationDbContext dbContext, IValidator<Invite> inviteValidator, IValidator<GetAllInvitesOptions> getAllInvitesOptionsValidator)
    {
        _dbContext = dbContext;
        _inviteValidator = inviteValidator;
        _getAllInvitesOptionsValidator = getAllInvitesOptionsValidator;
    }

    public async Task<bool> CreateAsync(Invite invite, CancellationToken cancellationToken = default)
    {
        await _inviteValidator.ValidateAndThrowAsync(invite, cancellationToken);

        await _dbContext.Invites.AddAsync(invite, cancellationToken);

        var result = await _dbContext.SaveChangesAsync(cancellationToken);

        return result > 0;
    }

    public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var invite = await _dbContext.Invites.FindAsync([id], cancellationToken);
        if (invite is null)
        {
            return false;
        }

        _dbContext.Invites.Remove(invite);

        var result = await _dbContext.SaveChangesAsync(cancellationToken);

        return result > 0;
    }

    public async Task<Invite?> GenerateTokenAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var invite = await _dbContext.Invites.FindAsync([id], cancellationToken);
        if (invite is null)
        {
            return null;
        }

        invite.Token = Guid.NewGuid();

        await _dbContext.SaveChangesAsync(cancellationToken);

        return invite;
    }
    
    public async Task<PaginatedList<Invite>> GetAllAsync(GetAllInvitesOptions options, CancellationToken cancellationToken = default)
    {
        await _getAllInvitesOptionsValidator.ValidateAndThrowAsync(options, cancellationToken);

        var query = _dbContext.Invites.AsQueryable();

        query = query.Where(o => o.EventId == options.EventId);

        if (!string.IsNullOrWhiteSpace(options.Email))
        {
            query = query.Where(o => o.Email.Contains(options.Email));
        }

        if (!string.IsNullOrWhiteSpace(options.HouseholdName))
        {
            query = query.Where(o => o.HouseholdName.Contains(options.HouseholdName));
        }

        if (!string.IsNullOrWhiteSpace(options.SortField))
        {
            query = options.SortField switch
            {
                "householdname" => options.SortOrder == SortOrder.Ascending ? query.OrderBy(o => o.HouseholdName).ThenBy(o => o.Id) : query.OrderByDescending(o => o.HouseholdName).ThenByDescending(o => o.Id),
                "email" => options.SortOrder == SortOrder.Ascending ? query.OrderBy(o => o.Email).ThenBy(o => o.Id) : query.OrderByDescending(o => o.Email).ThenByDescending(o => o.Id),
                _ => options.SortOrder == SortOrder.Ascending ? query.OrderBy(o => o.Id) : query.OrderByDescending(o => o.Id),
            };
        }

        var count = await query.CountAsync(cancellationToken);
        var items = await query.Skip((options.PageNumber - 1) * options.PageSize).Take(options.PageSize).ToListAsync(cancellationToken);

        return PaginatedList<Invite>.Create(items, count, options.PageNumber, options.PageSize);
    }

    public async Task<IEnumerable<Invite>> GetByEventIdAsync(Guid eventId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Invites.Where(p => p.EventId == eventId).ToListAsync(cancellationToken);
    }

    public async Task<Invite?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Invites.SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<Invite?> UpdateAsync(Invite invite, CancellationToken cancellationToken = default)
    {
        await _inviteValidator.ValidateAndThrowAsync(invite, cancellationToken);

        var existingInvite = await _dbContext.Invites.FindAsync([invite.Id], cancellationToken);
        if (existingInvite is null)
        {
            return null;
        }

        _dbContext.Invites.Entry(existingInvite).CurrentValues.SetValues(invite);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return invite;
    }
}
