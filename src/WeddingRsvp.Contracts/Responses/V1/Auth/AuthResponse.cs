namespace WeddingRsvp.Contracts.Responses.V1.Auth;

public sealed record AuthResponse(string AccessToken,
                                  string RefreshToken);
