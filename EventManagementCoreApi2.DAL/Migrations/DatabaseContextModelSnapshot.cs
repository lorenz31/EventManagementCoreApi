﻿// <auto-generated />
using EventManagementCoreApi2.DAL.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace EventManagementCoreApi2.DAL.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EventManagementCoreApi2.Core.Identity.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("EventManagementCoreApi2.Core.Models.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ApplicationUserId");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("VARCHAR(MAX)")
                        .HasMaxLength(-1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(MAX)")
                        .HasMaxLength(-1);

                    b.Property<int>("Status")
                        .HasColumnType("INT");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("INT");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("EventManagementCoreApi2.Core.Models.EventAttendee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("EventId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(MAX)");

                    b.Property<int>("Status")
                        .HasColumnType("INT");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("EventAttendees");
                });

            modelBuilder.Entity("EventManagementCoreApi2.Core.Models.EventDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Detail")
                        .IsRequired()
                        .HasColumnType("VARCHAR(MAX)")
                        .HasMaxLength(-1);

                    b.Property<Guid?>("EventId");

                    b.Property<DateTime>("Time")
                        .HasColumnType("DATETIME");

                    b.HasKey("Id");

                    b.HasIndex("EventId")
                        .IsUnique()
                        .HasFilter("[EventId] IS NOT NULL");

                    b.ToTable("EventDetail");
                });

            modelBuilder.Entity("EventManagementCoreApi2.Core.Models.Event", b =>
                {
                    b.HasOne("EventManagementCoreApi2.Core.Identity.ApplicationUser")
                        .WithMany("Events")
                        .HasForeignKey("ApplicationUserId");
                });

            modelBuilder.Entity("EventManagementCoreApi2.Core.Models.EventAttendee", b =>
                {
                    b.HasOne("EventManagementCoreApi2.Core.Models.Event")
                        .WithMany("Attendees")
                        .HasForeignKey("EventId");
                });

            modelBuilder.Entity("EventManagementCoreApi2.Core.Models.EventDetail", b =>
                {
                    b.HasOne("EventManagementCoreApi2.Core.Models.Event", "Event")
                        .WithOne("Detail")
                        .HasForeignKey("EventManagementCoreApi2.Core.Models.EventDetail", "EventId");
                });
#pragma warning restore 612, 618
        }
    }
}