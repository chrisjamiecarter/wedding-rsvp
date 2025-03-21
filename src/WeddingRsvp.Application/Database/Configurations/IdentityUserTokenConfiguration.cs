using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeddingRsvp.Application.Database.Constants;

namespace WeddingRsvp.Application.Database.Configurations;

/// <summary>
/// Configures an <see cref="IdentityUserToken"/> table definition in the database.
/// </summary>
internal class IdentityUserTokenConfiguration : IEntityTypeConfiguration<IdentityUserToken<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserToken<string>> builder)
    {
        builder.ToTable(TableConstants.ApplicationUserToken, SchemaConstants.Auth);
    }
}
