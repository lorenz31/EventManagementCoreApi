using EventManagementCoreApi2.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagementCoreApi2.DAL.DataContext.Configuration
{
    public class EventAttendeeConfiguration : IEntityTypeConfiguration<EventAttendee>
    {
        public void Configure(EntityTypeBuilder<EventAttendee> builder)
        {
            builder.HasKey(e => e.Id);

            builder
                .Property(e => e.Name)
                .HasColumnType("VARCHAR(MAX)")
                .IsRequired(true);

            builder
                .Property(e => e.Status)
                .HasColumnType("INT")
                .IsRequired(true);
        }
    }
}
