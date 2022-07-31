using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentForum.BusinessLogic.Abstractions;
using StudentForum.BusinessLogic.Models.Manage;
using StudentForum.Data.Entities.Account;
using StudentForum.WebUI.Helpers.Image;
using StudentForum.WebUI.Models.Manage;

namespace StudentForum.WebUI.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private readonly IAccountService _accountService;

        private readonly IMapper _mapper;

        public ManageController(IAccountService accountService,
            IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpGet("profile")]
        public IActionResult Profile()
        {
            return View();
        }

        [HttpGet]
        public async Task<PartialViewResult> Load()
        {
            var userId = _accountService.GetUserId();
            var user = await _accountService.GetUserById(userId);

            var profileModelView = _mapper.Map<User, ProfileModelView>(user);

            return PartialView(profileModelView);
        }

        [HttpGet]
        public PartialViewResult UpdatePhoto()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePhoto(PhotoUpdateModelView modal)
        {
            if (ModelState.IsValid)
            {
                await _accountService.UdpatePhoto(await modal.Photo.ConvertPhotoToBytes());

                return Json(new { result = true });
            }

            return Json(new 
            { 
                result = false,
                message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))
            });
        }

        [HttpGet]
        public async Task<PartialViewResult> Update()
        {
            var userId = _accountService.GetUserId();
            var user = await _accountService.GetUserById(userId);

            var userUpdate = _mapper.Map<User, UserUpdateModelView>(user);

            return PartialView(userUpdate);
        }

        [HttpPost]
        public async Task<JsonResult> Update(UserUpdateModelView model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    result = false,
                    message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))
                });
            }

            try
            {
                var userDto = _mapper.Map<UserUpdateModelView, UserDto>(model);

                await _accountService.Update(userDto);
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message });
            }

            return Json(new { result = true });
        }
    }
}
