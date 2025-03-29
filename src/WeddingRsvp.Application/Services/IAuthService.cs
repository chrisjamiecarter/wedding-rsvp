﻿using WeddingRsvp.Application.Entities;

namespace WeddingRsvp.Application.Services;

public interface IAuthService
{
    Task<ApplicationUser?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<bool> IsAdminAsync(ApplicationUser? user, CancellationToken cancellationToken = default);
    Task LogoutAsync(CancellationToken cancellationToken = default);
}