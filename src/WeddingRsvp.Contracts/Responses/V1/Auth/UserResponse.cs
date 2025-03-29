namespace WeddingRsvp.Contracts.Responses.V1.Auth;

public sealed record UserResponse(string Id, string Email, bool IsAdmin);
