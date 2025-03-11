﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeddingRsvp.Application.Database.Constants;
using WeddingRsvp.Application.Entities;

namespace WeddingRsvp.Application.Database.Configurations;

internal sealed class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.ToTable(TableConstants.Event, SchemaConstants.Core);

        builder.HasKey(pk =>  pk.Id);

        builder.Property(p => p.Name)
               .IsRequired();

        builder.Property(p => p.Venue)
               .IsRequired();

        builder.Property(p => p.Address)
               .IsRequired();

        builder.Property(p => p.Date)
               .IsRequired();

        builder.Property(p => p.Time)
               .IsRequired();
    }
}
