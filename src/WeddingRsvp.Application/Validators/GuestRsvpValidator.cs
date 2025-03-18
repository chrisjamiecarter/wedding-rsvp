using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WeddingRsvp.Application.Database;
using WeddingRsvp.Application.Enums;
using WeddingRsvp.Application.Models;

namespace WeddingRsvp.Application.Validators;

public sealed class GuestRsvpValidator : AbstractValidator<GuestRsvp>
{
    private static readonly RsvpStatus[] ValidRsvpStatus =
    [
        RsvpStatus.Attending,
        RsvpStatus.NotAttending,
    ];
    private readonly ApplicationDbContext _dbContext;

    public GuestRsvpValidator(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(x => x.Id)
            .MustAsync(ValidateGuestAsync)
            .WithMessage(x => $"No guest found for id: {x.Id}");

        RuleFor(x => x.RsvpStatus)
            .Must(x => ValidRsvpStatus.Contains(x))
            .WithMessage($"RSVP status must be one of the following: {string.Join(", ", ValidRsvpStatus)}");

        RuleFor(x => x.MainFoodOptionId)
            .NotEmpty()
            .When(x => x.RsvpStatus == RsvpStatus.Attending)
            .WithMessage("A main food option is required when attending");

        RuleFor(x => x.DessertFoodOptionId)
            .NotEmpty()
            .When(x => x.RsvpStatus == RsvpStatus.Attending)
            .WithMessage("A dessert food option is required when attending");
    }

    private async Task<bool> ValidateGuestAsync(GuestRsvp guestRsvp, Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Guests.AnyAsync(i => i.Id == id, cancellationToken);
    }
}
