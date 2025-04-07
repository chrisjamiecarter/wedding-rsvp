using FluentValidation;
using WeddingRsvp.Application.Entities;
using WeddingRsvp.Application.Enums;

namespace WeddingRsvp.Application.Validators;

public sealed class FoodOptionValidator : AbstractValidator<FoodOption>
{
    private static readonly FoodType[] ValidFoodTypes =
    [
        FoodType.Breakfast,
        FoodType.Main,
    ];

    public FoodOptionValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.EventId)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.FoodType)
            .Must(x => ValidFoodTypes.Contains(x))
            .WithMessage($"Food type must be one of the following: {string.Join(", ", ValidFoodTypes)}");
    }
}
