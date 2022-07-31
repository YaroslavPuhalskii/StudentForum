#nullable disable
using System.ComponentModel.DataAnnotations;

namespace StudentForum.WebUI.Models.Manage
{
    public class ChangePasswordModelView
    {
        [Required(ErrorMessage = "Current password is required!")]
        [DataType(DataType.Password, ErrorMessage = "Data is not password!")]
        [Display(Name = "Current password")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "New password is required!")]
        [DataType(DataType.Password, ErrorMessage = "Data is not password!")]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm password is required!")]
        [DataType(DataType.Password, ErrorMessage = "Data is not password!")]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match!")]
        public string ConfirmNewPassword { get; set; }
    }
}
