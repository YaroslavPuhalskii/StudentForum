#nullable disable
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using StudentForum.BusinessLogic.Abstractions;
using StudentForum.BusinessLogic.Models.Account;
using StudentForum.BusinessLogic.Models.Manage;
using StudentForum.Data.Entities.Account;
using StudentForum.DataAccess.Contracts.Account;
using System.Security.Claims;

namespace StudentForum.BusinessLogic.Services
{
    internal class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        private readonly IHttpContextAccessor _httpContext;

        private readonly IMapper _mapper;

        public AccountService(IAccountRepository accountRepository,
            IHttpContextAccessor httpContext,
            IMapper mapper)
        {
            _accountRepository = accountRepository;
            _httpContext = httpContext;
            _mapper = mapper;
        }

        public async Task<IdentityResult> Register(RegisterDto model)
        {
            if (model == null)
            {
                throw new ArgumentNullException($"{nameof(model)} can't be null!");
            }

            var user = _mapper.Map<RegisterDto, User>(model);

            var result = await _accountRepository.CreateUser(user, model.Password);

            if (result.Succeeded)
            {
                var defaultrole = await _accountRepository.FindRoleByName("User");

                if (defaultrole != null)
                {
                    result = await _accountRepository.AddToRole(user, defaultrole.Name);
                }
            }

            return result;
        }

        public async Task<SignInResult> Login(LoginDto model)
        {
            if (model == null)
            {
                throw new ArgumentNullException($"{nameof(model)} can't be null!");
            }

            var result = await _accountRepository.PasswordSignIn(model.Email, model.Password, model.RememberMe);

            return result;
        }

        public async Task SignOut()
        {
            await _accountRepository.SignOut();
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
    }
}
