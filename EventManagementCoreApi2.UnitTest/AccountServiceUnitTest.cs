using EventManagementCoreApi2.Core.Identity;
using EventManagementCoreApi2.DAL.DataContext;
using EventManagementCoreApi2.DAL.Repository;
using EventManagementCoreApi2.Helpers;
using EventManagementCoreApi2.Security.Helper;

using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace EventManagementCoreApi2.UnitTest
{
    [TestClass]
    public class AccountServiceUnitTest
    {
        GenericRepository<ApplicationUser> repo;
        DatabaseContext db;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>();
            options.UseSqlServer(DbConnectionStringHelper.CONNECTIONSTRING);

            db = new DatabaseContext(options.Options);
            repo = new GenericRepository<ApplicationUser>(db);
        }

        [TestMethod]
        public async Task Account_RegisterUserAsync_Test()
        {
            var appUser = new ApplicationUser
            {
                Email = "user2@gmail.com",
                Password = "Demo123!",
                DateCreated = DateTime.UtcNow
            };

            if (string.IsNullOrEmpty(appUser.Email))
                Assert.Fail("Email address empty");

            if (string.IsNullOrEmpty(appUser.Password))
                Assert.Fail("Password empty");

            var salt = PasswordHashHelper.GenerateSalt();
            var passwordHash = PasswordHashHelper.ComputeHash(appUser.Password, salt);

            appUser.Salt = Convert.ToBase64String(salt);
            appUser.Password = Convert.ToBase64String(passwordHash);

            var isRegistrationSuccessful = await repo.AddAsync(appUser);

            Assert.AreEqual(true, isRegistrationSuccessful);
        }
    }
}
