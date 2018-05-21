using EventManagementCoreApi2.Core.Models;
using EventManagementCoreApi2.Core.Identity;
using System.Threading.Tasks;

namespace EventManagementCoreApi2.Services.Interfaces
{
    public interface IAccountService
    {
        Task<bool> RegisterUserAsync(ApplicationUser model);
        Task<ApplicationUser> VerifyUserAsync(ApplicationUser model);
    }
}