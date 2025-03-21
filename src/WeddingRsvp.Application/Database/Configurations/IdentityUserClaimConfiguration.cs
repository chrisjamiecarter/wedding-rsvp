using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeddingRsvp.Application.Database.Constants;

namespace WeddingRsvp.Application.Database.Configurations;

/// <summary>
/// Configures an <see cref="IdentityUserClaim"/> table definition in the database.
/// </summary>
internal class IdentityUserClaimConfiguration : IEntityTypeConfiguration<IdentityUserClaim<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserClaim<string>> builder)
    {
        builder.ToTable(TableConstants.ApplicationUserClaim, SchemaConstants.Auth);
    }
}
