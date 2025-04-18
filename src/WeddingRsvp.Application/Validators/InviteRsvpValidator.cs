using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WeddingRsvp.Application.Database;
using WeddingRsvp.Application.Models;

namespace WeddingRsvp.Application.Validators;

public sealed class InviteRsvpValidator : AbstractValidator<InviteRsvp>
{
    private readonly ApplicationDbContext _dbContext;

    public InviteRsvpValidator(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Token)
            .MustAsync(ValidateTokenAsync)
            .WithMessage("Invalid invite token");

        RuleFor(x => x.Guests)
            .NotEmpty();

        RuleForEach(x => x.Guests)
            .SetValidator(new GuestRsvpValidator(dbContext));
    }

    private async Task<bool> ValidateTokenAsync(InviteRsvp inviteRsvp, Guid token, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Invites.AnyAsync(i => i.Id == inviteRsvp.Id && i.Token == token, cancellationToken);
    }
}
