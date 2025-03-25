namespace WeddingRsvp.Contracts.Responses.V1.Auth;

public sealed record MeResponse(IEnumerable<ClaimsResponse> Claims);
