using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeddingRsvp.Application.Database.Constants;
using WeddingRsvp.Application.Entities;

namespace WeddingRsvp.Application.Database.Configurations;

internal sealed class GuestConfiguration : IEntityTypeConfiguration<Guest>
{
    public void Configure(EntityTypeBuilder<Guest> builder)
    {
        builder.ToTable(TableConstants.Guest, SchemaConstants.Core);

        builder.HasKey(pk => pk.Id);

        builder.Property(p => p.Name)
               .IsRequired();

        builder.Property(p => p.RsvpStatus)
               .IsRequired()
               .HasConversion<int>();
    }
}
