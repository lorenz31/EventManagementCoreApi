using EventManagementCoreApi2.Core.Identity;
using EventManagementCoreApi2.Core.Models;
using EventManagementCoreApi2.Services.Interface;
using EventManagementCoreApi2.DAL.Repository;
using EventManagementCoreApi2.Security.Helper;

using System.Threading.Tasks;
using System;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;

namespace EventManagementCoreApi2.Services.Service
{
    public class AccountService : IAccountService
    {
        private IGenericRepository<ApplicationUser> _repo;
        private IEmailService _emailService;

        private IConfiguration _config;

        public AccountService(
            IGenericRepository<ApplicationUser> repo,
            IEmailService emailService,
            IConfiguration config)
        {
            _repo = repo;
            _emailService = emailService;
            _config = config;
        }

        public async Task<bool> RegisterUserAsync(ApplicationUser model)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(model.Email);

            if (string.IsNullOrEmpty(model.Email))
                return false;

            if (match.Success)
            {
                if (string.IsNullOrEmpty(model.Password))
                    return false;

                var salt = PasswordHashHelper.GenerateSalt();
                var passwordHash = PasswordHashHelper.ComputeHash(model.Password, salt);

                model.DateCreated = DateTime.UtcNow;
                model.Salt = Convert.ToBase64String(salt);
                model.Password = Convert.ToBase64String(passwordHash);

                var isRegistrationSuccessful = await _repo.AddAsync(model);

                if (isRegistrationSuccessful)
                {
                    _emailService.SendEmail(_config.GetSection("Sender").Value, model.Email, "Registration Successful", model.Email + ", " + _config.GetSection("EmailMessage").Value);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public async Task<ApplicationUser> VerifyUserAsync(ApplicationUser model)
        {
            var userInfo = await _repo.GetUserInfoAsync(u => u.Email == model.Email);

            if (userInfo == null)
                return null;

            var salt = Convert.FromBase64String(userInfo.Salt);
            var hashPassword = Convert.FromBase64String(userInfo.Password);

            var isVerified = PasswordHashHelper.VerifyPassword(model.Password, salt, hashPassword);

            return isVerified ? userInfo : null;
        }
    }
}