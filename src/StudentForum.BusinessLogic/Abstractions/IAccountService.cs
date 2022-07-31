using Microsoft.AspNetCore.Identity;
using StudentForum.BusinessLogic.Models.Account;
using StudentForum.BusinessLogic.Models.Manage;
using StudentForum.Data.Entities.Account;

namespace StudentForum.BusinessLogic.Abstractions
{
    public interface IAccountService
    {
        Task<IdentityResult> Register(RegisterDto model);

        Task<SignInResult> Login(LoginDto model);

        Task SignOut();

        string GetUserId();

        Task<User> GetUserById(string id);

        Task<IdentityResult> UdpatePhoto(byte[] photo);

        Task<IdentityResult> Update(UserDto model);
    }
}
