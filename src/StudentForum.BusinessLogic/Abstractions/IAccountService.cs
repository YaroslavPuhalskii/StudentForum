using Microsoft.AspNetCore.Identity;
using StudentForum.BusinessLogic.Models.Account;

namespace StudentForum.BusinessLogic.Abstractions
{
    public interface IAccountService
    {
        Task<IdentityResult> Register(RegisterDto model);

        Task<SignInResult> Login(LoginDto model);

        Task SignOut();
    }
}
