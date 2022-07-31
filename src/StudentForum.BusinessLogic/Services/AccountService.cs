#nullable disable
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using StudentForum.BusinessLogic.Abstractions;
using StudentForum.BusinessLogic.Models.Account;
using StudentForum.Data.Entities.Account;
using StudentForum.DataAccess.Contracts.Account;

namespace StudentForum.BusinessLogic.Services
{
    internal class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        private readonly IMapper _mapper;

        public AccountService(IAccountRepository accountRepository,
            IMapper mapper)
        {
            _accountRepository = accountRepository;
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
    }
}
