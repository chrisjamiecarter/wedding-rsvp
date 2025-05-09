﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeddingRsvp.Application.Database.Constants;

namespace WeddingRsvp.Application.Database.Configurations;

/// <summary>
/// Configures an <see cref="IdentityRoleClaim"/> table definition in the database.
/// </summary>
internal class IdentityRoleClaimConfiguration : IEntityTypeConfiguration<IdentityRoleClaim<string>>
{
    public void Configure(EntityTypeBuilder<IdentityRoleClaim<string>> builder)
    {
        builder.ToTable(TableConstants.RoleClaim, SchemaConstants.Auth);
    }
}
