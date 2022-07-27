using Microsoft.AspNetCore.Identity;
using StudentForum.Data.Entities.Account;

namespace StudentForum.DataAccess.Contracts.Account
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateUser(User user, string password);

        Task<SignInResult> PasswordSignIn(string email, string password, bool rememberMe);

        Task SignOut();

        Task<User> GetUserById(string id);

        Task<IdentityResult> ChangePassword(User user, string currentPassword, string newPassword);

        Task<IdentityResult> Update(User user);

        Task<IdentityRole> FindRoleByName(string name);

        Task<IdentityResult> AddToRole(User user, string role);
    }
}
