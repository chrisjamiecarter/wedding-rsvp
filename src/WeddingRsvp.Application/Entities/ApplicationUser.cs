﻿using Microsoft.AspNetCore.Identity;

namespace WeddingRsvp.Application.Entities;

public sealed class ApplicationUser : IdentityUser
{
    public ICollection<Event>? Events { get; }
}
