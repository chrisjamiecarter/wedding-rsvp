using WeddingRsvp.Application.Models;
using WeddingRsvp.Contracts.Responses.V1;

namespace WeddingRsvp.Api.Mappings.V1;

public static class TokenMapping
{
    public static TokenResponse ToResponse(this Token token)
    {
        return new TokenResponse(token.Value);
    }
}
