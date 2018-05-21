using EventManagementCoreApi2.Core.Identity;
using EventManagementCoreApi2.DAL.DataContext;
using EventManagementCoreApi2.DAL.Repository;
using EventManagementCoreApi2.Helpers;
using EventManagementCoreApi2.Security.Helper;
using EventManagementCoreApi2.Security.JWT;
using EventManagementCoreApi2.Services.Interfaces;
using EventManagementCoreApi2.Services.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EventManagementCoreApi2.UnitTest
{
    [TestClass]
    public class AccountServiceUnitTest
    {
        GenericRepository<ApplicationUser> repo;
        DatabaseContext db;

        AccountService accService;
        EmailService emailService;

        IConfiguration config;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>();
            options.UseSqlServer(DbConnectionStringHelper.CONNECTIONSTRING);

            db = new DatabaseContext(options.Options);

            accService = new AccountService(new GenericRepository<ApplicationUser>(db), new EmailService(), config);
            repo = new GenericRepository<ApplicationUser>(db);
        }

        [TestMethod]
        public async Task Account_RegisterUserAsync_Test()
        {
            string from = "developer.lorenz@gmail.com";
            string to = "lorenz53192@outlook.ph";
            string subject = "Event Management Registration";
            string body = "You have successfully registered at Event Management.";

            var appUser = new ApplicationUser
            {
                Email = to,
                Password = "Demo123!",
                DateCreated = DateTime.UtcNow
            };

            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(to);

            if (string.IsNullOrEmpty(appUser.Email))
                Assert.Fail("Email address empty");

            if (match.Success)
            {
                if (string.IsNullOrEmpty(appUser.Password))
                    Assert.Fail("Password empty");

                var salt = PasswordHashHelper.GenerateSalt();
                var passwordHash = PasswordHashHelper.ComputeHash(appUser.Password, salt);

                appUser.Salt = Convert.ToBase64String(salt);
                appUser.Password = Convert.ToBase64String(passwordHash);

                var isRegistrationSuccessful = await repo.AddAsync(appUser);

                if (isRegistrationSuccessful)
                {
                    emailService = new EmailService();
                    emailService.SendEmail(from, to, subject, body);

                    Assert.AreEqual(true, isRegistrationSuccessful);
                }
                else
                {
                    Assert.Fail();
                }
            }
            else
            {
                Assert.Fail("Invalid email.");
            }
        }

        [TestMethod]
        public async Task Account_VerifyUserAsync_Test()
        {
            var user = new ApplicationUser
            {
                Email = "user1@gmail.com",
                Password = "Demo123!"
            };

            var userInfo = await repo.GetUserInfoAsync(u => u.Email == user.Email);

            if (userInfo == null)
                Assert.Fail();

            var salt = Convert.FromBase64String(userInfo.Salt);
            var hashPassword = Convert.FromBase64String(userInfo.Password);

            var isVerified = PasswordHashHelper.VerifyPassword(user.Password, salt, hashPassword);

            Assert.AreEqual(true, isVerified);
        }

        [TestMethod]
        public async Task Account_ObtainAccessTokenAsync_Test()
        {
            var user = new ApplicationUser
            {
                Email = "user1@gmail.com",
                Password = "Demo123!"
            };

            var userVerified = await accService.VerifyUserAsync(user);

            Assert.IsNotNull(userVerified, "User not verified.");

            if (userVerified != null)
            {
                var token = new JwtTokenBuilder()
                                .AddSecurityKey(JwtSecurityKey.Create(config.GetSection("JwtSettings:SecurityKey").Value))
                                .AddSubject(user.Email)
                                .AddIssuer(config.GetSection("AppConfiguration:Issuer").Value)
                                .AddAudience(config.GetSection("AppConfiguration:Issuer").Value)
                                //.AddClaim("SellerId", userVerified.Id.ToString())
                                .AddExpiry(10)
                                .Build();

                JwtTokenModel tokenModel = new JwtTokenModel
                {
                    AccessToken = token.Value,
                    UserId = userVerified.Id.ToString()
                };

                Assert.IsNotNull(tokenModel.AccessToken);
            }
        }
    }
}
