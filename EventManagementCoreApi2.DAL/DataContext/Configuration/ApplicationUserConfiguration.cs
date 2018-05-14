using EventManagementCoreApi2.Core.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagementCoreApi2.DAL.DataContext.Configuration
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(u => u.Id);

            builder
                .Property(u => u.Email)
                .IsRequired(true);

            builder
                .Property(u => u.Password)
                .IsRequired(true);

            builder
                .Property(u => u.DateCreated)
                .IsRequired(true);

            builder.HasMany(u => u.Events);
        }
    }
}
