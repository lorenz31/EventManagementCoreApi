using EventManagementCoreApi2.Core.Identity;
using EventManagementCoreApi2.Core.Models;
using EventManagementCoreApi2.DAL.DataContext.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagementCoreApi2.DAL.DataContext
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> context) : base(context)
        {
        }

        public DbSet<ApplicationUser> User { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventDetail> EventDetail { get; set; }
        public DbSet<EventAttendee> EventAttendees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Application User Configuration
            modelBuilder
                .Entity<ApplicationUser>()
                .HasKey(u => u.Id);

            modelBuilder
                .Entity<ApplicationUser>()
                .Property(u => u.Email)
                .IsRequired(true);

            modelBuilder
                .Entity<ApplicationUser>()
                .Property(u => u.Password)
                .IsRequired(true);

            modelBuilder
                .Entity<ApplicationUser>()
                .Property(u => u.Salt)
                .IsRequired(true);

            modelBuilder
                .Entity<ApplicationUser>()
                .Property(u => u.DateCreated)
                .IsRequired(true);

            modelBuilder
                .Entity<ApplicationUser>()
                .HasMany(u => u.Events)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId);
            #endregion

            #region Event Configuration
            modelBuilder
                .Entity<Event>()
                .HasKey(e => e.Id);

            modelBuilder
                .Entity<Event>()
                .Property(e => e.Name)
                .IsRequired();

            modelBuilder
                .Entity<Event>()
                .Property(e => e.Location)
                .IsRequired(true);

            modelBuilder
                .Entity<Event>()
                .Property(e => e.Type)
                .IsRequired(true);

            modelBuilder
                .Entity<Event>()
                .Property(e => e.Status)
                .IsRequired(true);

            modelBuilder
                .Entity<Event>()
                .HasOne(e => e.Detail)
                .WithOne(e => e.Event)
                .HasForeignKey<EventDetail>(e => e.EventId)
                .IsRequired(true);
            #endregion

            #region Event Detail
            modelBuilder
                .Entity<EventDetail>()
                .HasKey(e => e.Id);

            modelBuilder
                .Entity<EventDetail>()
                .Property(e => e.Time)
                .IsRequired(true);

            modelBuilder
                .Entity<EventDetail>()
                .Property(e => e.Detail)
                .IsRequired(true);
            #endregion

            #region Event Attendee
            modelBuilder
                .Entity<EventAttendee>()
                .HasKey(e => e.Id);

            modelBuilder
                .Entity<EventAttendee>()
                .Property(e => e.Name)
                .IsRequired(true);

            modelBuilder
                .Entity<EventAttendee>()
                .Property(e => e.Status)
                .IsRequired(true);

            modelBuilder
                .Entity<EventAttendee>()
                .HasOne(e => e.Event)
                .WithMany(e => e.Attendees)
                .HasForeignKey(e => e.EventId)
                .IsRequired(true);
            #endregion
        }
    }
}
