namespace WeddingRsvp.Contracts.Requests.V1.Auth;

public sealed record LoginRequest(string Email,
                                  string Password);

