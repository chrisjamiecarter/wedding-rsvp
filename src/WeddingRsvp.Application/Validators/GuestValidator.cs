using FluentValidation;
using WeddingRsvp.Application.Entities;
using WeddingRsvp.Application.Enums;

namespace WeddingRsvp.Application.Validators;

public sealed class GuestValidator : AbstractValidator<Guest>
{
    private static readonly RsvpStatus[] ValidRsvpStatus =
    [
        RsvpStatus.AwaitingResponse,
        RsvpStatus.Attending,
        RsvpStatus.NotAttending,
    ];

    public GuestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.InviteId)
            .NotEmpty();

        RuleSet("Create", () =>
        {
            RuleFor(x => x.RsvpStatus)
                .NotNull();
        });

        RuleSet("Update", () =>
        {
            RuleFor(x => x.RsvpStatus)
                .Must(x => ValidRsvpStatus.Contains(x))
                .WithMessage($"RSVP status must be one of the following: {string.Join(", ", ValidRsvpStatus)}");
        });
    }
}
