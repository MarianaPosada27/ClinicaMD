using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using ClinicaMD.Web.Data.Entities;
using ClinicaMD.Web.Models;

namespace ClinicaMD.Web.Helpers
{
   
    public interface IUserHelper
    {
        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogoutAsync();

        Task<User> GetUserAsync(string email);

        Task<IdentityResult> AddUserAsync(User user, string password);

        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(User user, string roleName);

        Task<bool> IsUserInRoleAsync(User user, string roleName);
    }

}
