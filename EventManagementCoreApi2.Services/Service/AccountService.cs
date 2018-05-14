using EventManagementCoreApi2.Core.Identity;
using EventManagementCoreApi2.Core.Models;
using EventManagementCoreApi2.Services.Interfaces;
using EventManagementCoreApi2.DAL.Repository;
using EventManagementCoreApi2.Security.Helper;

using System.Threading.Tasks;
using System;

namespace EventManagementCoreApi2.Services.Service
{
    public class AccountService : IAccountService
    {
        private IGenericRepository<ApplicationUser> _repo;

        public AccountService(IGenericRepository<ApplicationUser> repo)
        {
            _repo = repo;
        }

        public async Task<bool> RegisterUserAsync(ApplicationUser model)
        {
            if (string.IsNullOrEmpty(model.Email))
                return false;

            if (string.IsNullOrEmpty(model.Password))
                return false;

            var salt = PasswordHashHelper.GenerateSalt();
            var passwordHash = PasswordHashHelper.ComputeHash(model.Password, salt);

            model.DateCreated = DateTime.UtcNow;
            model.Salt = Convert.ToBase64String(salt);
            model.Password = Convert.ToBase64String(passwordHash);

            var isRegistrationSuccessful = await _repo.AddAsync(model);

            return isRegistrationSuccessful ? true : false;
        }

        public async Task<ApplicationUser> VerifyUserAsync(ApplicationUser model)
        {
            var user = new ApplicationUser { Email = model.Email };

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