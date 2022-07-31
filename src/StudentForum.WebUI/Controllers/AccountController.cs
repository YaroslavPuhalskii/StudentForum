using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentForum.BusinessLogic.Abstractions;
using StudentForum.BusinessLogic.Models.Account;
using StudentForum.WebUI.Helpers.Image;
using StudentForum.WebUI.Models.Account;

namespace StudentForum.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        private readonly IMapper _mapper;

        public AccountController(IAccountService accountService,
            IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisteModelView model)
        {
            if (ModelState.IsValid)
            {
                model.CoverPhotoBytes = await model.CoverPhoto.ConvertPhotoToBytes();

                var registerDto = _mapper.Map<RegisteModelView, RegisterDto>(model);

                var result = await _accountService.Register(registerDto);

                if (!result.Succeeded)
                {
                    foreach (var errorMessage in result.Errors)
                    {
                        ModelState.AddModelError("", errorMessage.Description);
                    }

                    return View(model);
                }

                ModelState.Clear();
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModelView model)
        {
            if (ModelState.IsValid)
            {
                var loginDto = _mapper.Map<LoginModelView, LoginDto>(model);

                var result = await _accountService.Login(loginDto);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Invalid credentials");
            }

            return View(model);
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await _accountService.SignOut();

            return RedirectToAction("Login", "Account");
        }
    }
}
