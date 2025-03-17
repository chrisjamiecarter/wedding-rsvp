using FluentValidation;
using WeddingRsvp.Application.Enums;
using WeddingRsvp.Application.Models;

namespace WeddingRsvp.Application.Validators;

public sealed class GetAllEventFoodOptionsValidator : AbstractValidator<GetAllEventFoodOptionsOptions>
{
    private static readonly FoodType[] ValidFoodTypes =
    [
        FoodType.Breakfast,
        FoodType.Main,
    ];

    private static readonly string[] ValidSortFields =
    [
        "foodtype",
    ];

    public GetAllEventFoodOptionsValidator()
    {
        RuleFor(x => x.FoodType)
            .Must(x => x is null || ValidFoodTypes.Contains(x.Value))
            .WithMessage($"Food type must be one of the following: {string.Join(", ", ValidFoodTypes)}");

        RuleFor(x => x.SortField)
            .Must(x => x is null || ValidSortFields.Contains(x.ToLowerInvariant()))
            .WithMessage($"Sort field must be one of the following: {string.Join(", ", ValidSortFields)}");

        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 25)
            .WithMessage("You can get between 1 and 25 event food options per page");
    }
}
