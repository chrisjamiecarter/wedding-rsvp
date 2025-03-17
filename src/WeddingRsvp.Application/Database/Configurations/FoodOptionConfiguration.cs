using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeddingRsvp.Application.Database.Constants;
using WeddingRsvp.Application.Entities;

namespace WeddingRsvp.Application.Database.Configurations;

internal sealed class FoodOptionConfiguration : IEntityTypeConfiguration<FoodOption>
{
    public void Configure(EntityTypeBuilder<FoodOption> builder)
    {
        builder.ToTable(TableConstants.FoodOption, SchemaConstants.Core);

        builder.HasKey(pk => pk.Id);

        builder.Property(p => p.Name)
               .IsRequired();

        builder.Property(p => p.FoodType)
               .IsRequired()
               .HasConversion<int>();

        builder.HasMany(e => e.EventFoodOptions)
               .WithOne(o => o.FoodOption)
               .HasForeignKey(fk => fk.FoodOptionId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
