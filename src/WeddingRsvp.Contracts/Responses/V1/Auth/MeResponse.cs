namespace WeddingRsvp.Contracts.Responses.V1.Auth;

public sealed record MeResponse(string Id, string Email, bool IsAdmin);
