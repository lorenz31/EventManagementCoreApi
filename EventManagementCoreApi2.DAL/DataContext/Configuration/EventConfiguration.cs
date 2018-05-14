using EventManagementCoreApi2.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagementCoreApi2.DAL.DataContext.Configuration
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(e => e.Id);

            builder
                .Property(e => e.Name)
                .HasColumnType("VARCHAR(MAX)")
                .HasMaxLength(-1)
                .IsRequired(true);

            builder
                .Property(e => e.Location)
                .HasColumnType("VARCHAR(MAX)")
                .HasMaxLength(-1)
                .IsRequired(true);

            builder
                .Property(e => e.Type)
                .HasColumnType("INT")
                .IsRequired(true);

            builder
                .Property(e => e.Status)
                .HasColumnType("INT")
                .IsRequired(true);

            builder.HasOne(e => e.Detail);

            builder.HasMany(e => e.Attendees);
        }
    }
}
