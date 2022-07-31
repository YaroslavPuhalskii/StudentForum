#nullable disable
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using StudentForum.BusinessLogic.Abstractions;
using StudentForum.BusinessLogic.Models.Manage;
using StudentForum.Data.Entities.Account;
using StudentForum.DataAccess.Contracts.Account;
using System.Security.Claims;

namespace StudentForum.BusinessLogic.Services
{
    internal class ManageService : IManageService
    {
        private readonly IAccountRepository _accountRepository;

        private readonly IHttpContextAccessor _httpContext;

        public ManageService(IAccountRepository accountRepository,
            IHttpContextAccessor httpContext)
        {
            _accountRepository = accountRepository;

            _httpContext = httpContext;
        }

        public string GetUserId()
        {
            return _httpContext.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public async Task<User> GetUserById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException($"{id} can't be null or empty!");
            }

            return await _accountRepository.GetUserById(id);
        }

        public async Task<IdentityResult> UdpatePhoto(byte[] photo)
        {
            if (photo == null || photo.Length < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(photo), $"{nameof(photo)} can't be null!");
            }

            var userId = GetUserId();
            var user = await _accountRepository.GetUserById(userId);

            user.Photo = photo;

            return await _accountRepository.Update(user);
        }

        public async Task<IdentityResult> Update(UserDto model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var userId = GetUserId();
            var user = await _accountRepository.GetUserById(userId);

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;

            return await _accountRepository.Update(user);
        }

        public async Task<IdentityResult> ChangePassword(ChangePasswordDto model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var userId = GetUserId();
            var user = await _accountRepository.GetUserById(userId);

            return await _accountRepository.ChangePassword(user, model.CurrentPassword, model.NewPassword);
        }
    }
}
