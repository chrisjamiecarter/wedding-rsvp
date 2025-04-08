using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeddingRsvp.Application.Database.Constants;
using WeddingRsvp.Application.Entities;

namespace WeddingRsvp.Application.Database.Configurations;

/// <summary>
/// Configures an <see cref="ApplicationUser"/> table definition in the database.
/// </summary>
internal class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable(TableConstants.ApplicationUser, SchemaConstants.Auth);

        builder.HasMany(e => e.Events)
               .WithOne(o => o.User)
               .HasForeignKey(fk => fk.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
