using FluentValidation;
using WeddingRsvp.Application.Enums;
using WeddingRsvp.Application.Models;

namespace WeddingRsvp.Application.Validators;

public sealed class GetAllGuestsForEventOptionsValidator : AbstractValidator<GetAllGuestsForEventOptions>
{
    private static readonly string[] ValidSortFields =
    [
        "name",
        "rsvp",
        "household",
        "main",
        "dessert",
    ];

    private static readonly RsvpStatus[] ValidRsvpStatus =
    [
        RsvpStatus.AwaitingResponse,
        RsvpStatus.Attending,
        RsvpStatus.NotAttending,
    ];

    public GetAllGuestsForEventOptionsValidator()
    {
        RuleFor(x => x.RsvpStatus)
            .Must(x => x is null || ValidRsvpStatus.Contains(x.Value))
            .WithMessage($"RSVP status must be one of the following: {string.Join(", ", ValidRsvpStatus)}");

        RuleFor(x => x.SortField)
            .Must(x => x is null || ValidSortFields.Contains(x.ToLowerInvariant()))
            .WithMessage($"Sort field must be one of the following: {string.Join(", ", ValidSortFields)}");

        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 25)
            .WithMessage("You can get between 1 and 25 guests per page");
    }
}
