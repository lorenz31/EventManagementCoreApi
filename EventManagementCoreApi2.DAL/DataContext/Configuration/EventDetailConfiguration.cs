using EventManagementCoreApi2.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagementCoreApi2.DAL.DataContext.Configuration
{
    public class EventDetailConfiguration : IEntityTypeConfiguration<EventDetail>
    {
        public void Configure(EntityTypeBuilder<EventDetail> builder)
        {
            builder.HasKey(e => e.Id);

            builder
                .Property(e => e.Time)
                .HasColumnType("DATETIME")
                .IsRequired(true);

            builder
                .Property(e => e.Detail)
                .HasColumnType("VARCHAR(MAX)")
                .HasMaxLength(-1)
                .IsRequired(true);

            builder.HasOne(e => e.Event);
        }
    }
}
