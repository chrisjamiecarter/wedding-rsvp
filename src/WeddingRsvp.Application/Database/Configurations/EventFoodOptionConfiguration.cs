using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeddingRsvp.Application.Database.Constants;
using WeddingRsvp.Application.Entities;

namespace WeddingRsvp.Application.Database.Configurations;

internal sealed class EventFoodOptionConfiguration : IEntityTypeConfiguration<EventFoodOption>
{
    public void Configure(EntityTypeBuilder<EventFoodOption> builder)
    {
        builder.ToTable(TableConstants.EventFoodOption, SchemaConstants.Core);

        builder.HasKey(pk =>  pk.Id);
    }
}
