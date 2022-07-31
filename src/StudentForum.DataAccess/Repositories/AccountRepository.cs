using Microsoft.AspNetCore.Identity;
using StudentForum.Data.Entities.Account;
using StudentForum.DataAccess.Contracts.Account;

namespace StudentForum.DataAccess.Repositories
{
    internal class AccountRepository : IAccountRepository
    {
        private readonly UserManager<User> _userManager;

        private readonly SignInManager<User> _signInManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountRepository(UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> CreateUser(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);

            return result;
        }

        public async Task<SignInResult> PasswordSignIn(string email, string password, bool rememberMe)
        {
            return await _signInManager.PasswordSignInAsync(email, password, rememberMe, false);
        }

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityRole> FindRoleByName(string name)
        {
            return await _roleManager.FindByNameAsync(name);
        }

        public async Task<IdentityResult> AddToRole(User user, string role)
        {
            return await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<User> GetUserById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<IdentityResult> Update(User user)
        {
            return await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> ChangePassword(User user, string currentPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        }
    }
}
