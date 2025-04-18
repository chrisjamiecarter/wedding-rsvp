using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WeddingRsvp.Application.Database;
using WeddingRsvp.Application.Entities;

namespace WeddingRsvp.Application.Services;

internal class InviteService : IInviteService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IValidator<Invite> _inviteValidator;

    public InviteService(ApplicationDbContext dbContext, IValidator<Invite> inviteValidator)
    {
        _dbContext = dbContext;
        _inviteValidator = inviteValidator;
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
