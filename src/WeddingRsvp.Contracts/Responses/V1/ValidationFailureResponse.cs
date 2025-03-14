namespace WeddingRsvp.Contracts.Responses.V1;

public sealed record ValidationFailureResponse(IEnumerable<ValidationResponse> Errors);
