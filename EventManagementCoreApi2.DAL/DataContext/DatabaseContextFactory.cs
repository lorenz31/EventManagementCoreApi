using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagementCoreApi2.DAL.DataContext
{
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<DatabaseContext>();
            builder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=EventManagementCoreApiDB;Integrated Security=true;Trusted_Connection=True;MultipleActiveResultSets=True");

            return new DatabaseContext(builder.Options);
        }
    }
}
