#nullable disable
using System.ComponentModel.DataAnnotations;

namespace StudentForum.WebUI.Models.Account
{
    public class LoginModelView
    {
        [Required(ErrorMessage = "Email is required!")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Enter correct data.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [DataType(DataType.Password, ErrorMessage = "Enter correct data.")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
