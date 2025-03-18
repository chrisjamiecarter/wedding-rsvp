using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WeddingRsvp.Application.Database;
using WeddingRsvp.Application.Entities;
using WeddingRsvp.Application.Models;

namespace WeddingRsvp.Application.Services;

internal class InviteService : IInviteService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IValidator<Invite> _inviteValidator;
    private readonly IValidator<InviteRsvp> _inviteRsvpValidator;

    public InviteService(ApplicationDbContext dbContext, IValidator<Invite> inviteValidator, IValidator<InviteRsvp> inviteRsvpValidator)
    {
        _dbContext = dbContext;
        _inviteValidator = inviteValidator;
        _inviteRsvpValidator = inviteRsvpValidator;
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

        invite.UniqueLinkToken = Guid.NewGuid();

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

    public async Task<Invite?> SubmitRsvp(InviteRsvp inviteRsvp, CancellationToken cancellationToken = default)
    {
        await _inviteRsvpValidator.ValidateAndThrowAsync(inviteRsvp, cancellationToken);

        using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var invite = await _dbContext.Invites.Include(i => i.Guests).SingleOrDefaultAsync(i => i.Id == inviteRsvp.Id && i.UniqueLinkToken == inviteRsvp.Token, cancellationToken);
            if (invite == null)
            {
                throw new InvalidOperationException($"Invalid invite token");
            }

            var validGuestIds = invite.Guests!.Select(g => g.Id).ToHashSet();
            foreach (var guestRsvp in inviteRsvp.Guests)
            {
                if (!validGuestIds.Contains(guestRsvp.Id))
                {
                    throw new InvalidOperationException($"No guest found for id: {guestRsvp.Id}");
                }
            }

            foreach (var guestRsvp in inviteRsvp.Guests)
            {
                var guest = await _dbContext.Guests.FindAsync([guestRsvp.Id], cancellationToken);
                if (guest != null)
                {
                    guest.RsvpStatus = guestRsvp.RsvpStatus;
                    guest.MainFoodOptionId = guestRsvp.MainFoodOptionId;
                    guest.DessertFoodOptionId = guestRsvp.DessertFoodOptionId;
                }
            }

            invite.UniqueLinkToken = null;

            await _dbContext.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return invite;
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            return null;
        }
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
