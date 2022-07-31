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
        private readonly IManageService _manageService;

        private readonly IMapper _mapper;

        public ManageController(IManageService maanageService,
            IMapper mapper)
        {
            _manageService = maanageService;
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
            var userId = _manageService.GetUserId();
            var user = await _manageService.GetUserById(userId);

            var profileModelView = _mapper.Map<User, ProfileModelView>(user);

            return PartialView(profileModelView);
        }

        [HttpGet("update-photo")]
        public PartialViewResult UpdatePhoto()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePhoto(PhotoUpdateModelView modal)
        {
            if (ModelState.IsValid)
            {
                await _manageService.UdpatePhoto(await modal.Photo.ConvertPhotoToBytes());

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
            var userId = _manageService.GetUserId();
            var user = await _manageService.GetUserById(userId);

            var userUpdate = _mapper.Map<User, UserUpdateModelView>(user);

            return PartialView(userUpdate);
        }

        [HttpPost]
        public async Task<JsonResult> Update(UserUpdateModelView model)
        {
            if (ModelState.IsValid)
            {
                var userDto = _mapper.Map<UserUpdateModelView, UserDto>(model);

                await _manageService.Update(userDto);

                return Json(new { result = true });
            }

            return Json(new
            {
                result = false,
                message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))
            });
        }

        [HttpGet("change-password")]
        public PartialViewResult ChangePassword()
        {
            return PartialView();
        }

        [HttpPost("change-password")]
        public async Task<JsonResult> ChangePassword(ChangePasswordModelView model)
        {
            if (ModelState.IsValid)
            {
                var changePassword = _mapper.Map<ChangePasswordModelView, ChangePasswordDto>(model);

                var result = await _manageService.ChangePassword(changePassword);

                if (result.Succeeded)
                {
                    return Json(new { result = true });
                }

                ModelState.AddModelError("", "Invalid credentials!");
            }

            return Json(new 
            {
                result = false,
                message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))
            });
        }
    }
}
