using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeddingRsvp.Application.Database.Constants;
using WeddingRsvp.Application.Entities;

namespace WeddingRsvp.Application.Database.Configurations;

internal sealed class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable(TableConstants.RefreshToken, SchemaConstants.Auth);

        builder.HasKey(pk => pk.Id);

        builder.Property(p => p.Value)
               .IsRequired()
               .HasMaxLength(200);

        builder.HasIndex(uq => uq.Value)
               .IsUnique();

        builder.HasOne(r => r.User)
               .WithMany()
               .HasForeignKey(fk => fk.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
