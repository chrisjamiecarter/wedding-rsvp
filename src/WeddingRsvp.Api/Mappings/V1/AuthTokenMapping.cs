using WeddingRsvp.Application.Models;
using WeddingRsvp.Contracts.Responses.V1.Auth;

namespace WeddingRsvp.Api.Mappings.V1;

public static class AuthTokenMapping
{
    public static AuthResponse ToResponse(this AuthToken token)
    {
        return new AuthResponse(token.AccessToken, token.RefreshToken.Value);
    }
}
