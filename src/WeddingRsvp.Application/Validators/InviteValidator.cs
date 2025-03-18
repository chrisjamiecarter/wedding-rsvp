using FluentValidation;
using WeddingRsvp.Application.Entities;

namespace WeddingRsvp.Application.Validators;

public sealed class InviteValidator : AbstractValidator<Invite>
{
    public InviteValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Email)
            .NotEmpty();

        RuleFor(x => x.HouseholdName)
            .NotEmpty();

        RuleFor(x => x.EventId)
            .NotEmpty();
    }
}
