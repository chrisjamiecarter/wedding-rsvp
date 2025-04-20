using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WeddingRsvp.Application.Database;
using WeddingRsvp.Application.Entities;
using WeddingRsvp.Application.Enums;
using WeddingRsvp.Application.Models;
using WeddingRsvp.Application.Shared;

namespace WeddingRsvp.Application.Services;

internal class RsvpService : IRsvpService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IValidator<InviteRsvp> _inviteRsvpValidator;

    public RsvpService(ApplicationDbContext dbContext, IValidator<InviteRsvp> inviteRsvpValidator)
    {
        _dbContext = dbContext;
        _inviteRsvpValidator = inviteRsvpValidator;
    }

    public async Task<Invite?> GetAsync(Guid inviteId, Guid token, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Invites.SingleOrDefaultAsync(p => p.Id == inviteId && p.Token == token, cancellationToken);
    }

    public async Task<IEnumerable<FoodOption>> GetFoodOptionsAsync(Guid inviteId, Guid token, FoodType foodType, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Invites.Where(i => i.Id == inviteId && i.Token == token)
                                       .Join(_dbContext.FoodOptions, invite => invite.EventId, foodOption => foodOption.EventId, (invite, foodOption) => foodOption)
                                       .Where(f => f.FoodType == foodType)
                                       .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Guest>> GetGuestsAsync(Guid inviteId, Guid token, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Guests.Include(g => g.Invite).Where(p => p.InviteId == inviteId && p.Invite!.Token == token).ToListAsync(cancellationToken);
    }

    public async Task<Result> SubmitAsync(InviteRsvp inviteRsvp, CancellationToken cancellationToken = default)
    {
        await _inviteRsvpValidator.ValidateAndThrowAsync(inviteRsvp, cancellationToken);

        using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var invite = await _dbContext.Invites.Include(i => i.Guests).SingleOrDefaultAsync(i => i.Id == inviteRsvp.Id && i.Token == inviteRsvp.Token, cancellationToken);
            if (invite is null)
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

            invite.Token = null;

            await _dbContext.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return Result.Success();
        }
        catch (Exception exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            return Result.Failure(new("RsvpService.Exception", exception.Message));
        }
    }
}
