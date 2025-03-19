using WeddingRsvp.Application.Entities;

namespace WeddingRsvp.Application.Models;

public sealed record AuthToken(string AccessToken, RefreshToken RefreshToken);
