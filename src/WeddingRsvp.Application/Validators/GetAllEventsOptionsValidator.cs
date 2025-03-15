using FluentValidation;
using WeddingRsvp.Application.Models;

namespace WeddingRsvp.Application.Validators;

public class GetAllEventsOptionsValidator : AbstractValidator<GetAllEventsOptions>
{
    private static readonly string[] ValidSortFields =
    {
        "name",
        "venue",
        "date",
    };

    public GetAllEventsOptionsValidator()
    {
        RuleFor(x => x.SortField)
            .Must(x => x is null || ValidSortFields.Contains(x.ToLowerInvariant()))
            .WithMessage($"Sort field must be one of the following: {string.Join(", ", ValidSortFields)}");

        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 25)
            .WithMessage("You can get between 1 and 25 events per page");
    }
}
