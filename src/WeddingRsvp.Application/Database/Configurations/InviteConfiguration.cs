using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeddingRsvp.Application.Database.Constants;
using WeddingRsvp.Application.Entities;

namespace WeddingRsvp.Application.Database.Configurations;

internal sealed class InviteConfiguration : IEntityTypeConfiguration<Invite>
{
    public void Configure(EntityTypeBuilder<Invite> builder)
    {
        builder.ToTable(TableConstants.Invite, SchemaConstants.Core);

        builder.HasKey(pk =>  pk.Id);

        builder.Property(p => p.Email)
               .IsRequired();

        builder.Property(p => p.HouseholdName)
               .IsRequired();
    }
}
