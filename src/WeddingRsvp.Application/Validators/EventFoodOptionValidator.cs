using FluentValidation;
using WeddingRsvp.Application.Entities;

namespace WeddingRsvp.Application.Validators;

public sealed class EventFoodOptionValidator : AbstractValidator<EventFoodOption>
{
    public EventFoodOptionValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.EventId)
            .NotEmpty();

        RuleFor(x => x.FoodOptionId)
            .NotEmpty();
    }
}
