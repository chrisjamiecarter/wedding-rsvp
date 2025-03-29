using WeddingRsvp.Application.Entities;
using WeddingRsvp.Contracts.Responses.V1.Auth;

namespace WeddingRsvp.Api.Mappings.V1;

public static class AuthMapping
{
    public static MeResponse ToResponse(this ApplicationUser? entity, bool isAdmin)
    {
        return entity is null
            ? new MeResponse(null)
            : new MeResponse(new UserResponse(entity.Id,
                                              entity.Email!,
                                              isAdmin));
    }
}
