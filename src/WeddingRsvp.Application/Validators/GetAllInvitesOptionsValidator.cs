using FluentValidation;
using WeddingRsvp.Application.Models;

namespace WeddingRsvp.Application.Validators;

public sealed class GetAllInvitesOptionsValidator : AbstractValidator<GetAllInvitesOptions>
{
    private static readonly string[] ValidSortFields =
    {
        "householdname",
        "email",
    };

    public GetAllInvitesOptionsValidator()
    {
        RuleFor(x => x.EventId)
            .NotEmpty();

        RuleFor(x => x.SortField)
            .Must(x => x is null || ValidSortFields.Contains(x.ToLowerInvariant()))
            .WithMessage($"Sort field must be one of the following: {string.Join(", ", ValidSortFields)}");

        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 25)
            .WithMessage("You can get between 1 and 25 invites per page");
    }
}
