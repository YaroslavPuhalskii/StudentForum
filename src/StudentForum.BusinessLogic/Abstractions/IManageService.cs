using Microsoft.AspNetCore.Identity;
using StudentForum.BusinessLogic.Models.Manage;
using StudentForum.Data.Entities.Account;

namespace StudentForum.BusinessLogic.Abstractions
{
    public interface IManageService
    {
        string GetUserId();

        Task<User> GetUserById(string id);

        Task<IdentityResult> UdpatePhoto(byte[] photo);

        Task<IdentityResult> Update(UserDto model);

        Task<IdentityResult> ChangePassword(ChangePasswordDto model);
    }
}
