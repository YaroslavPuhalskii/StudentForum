#nullable disable
using System.ComponentModel.DataAnnotations;

namespace StudentForum.WebUI.Models.Account
{
    public class RegisteModelView
    {
        [Required(ErrorMessage = "First name is required!")]
        [Display(Name = "First name")]
        [StringLength(50, ErrorMessage = "First name length must be between 1 and 50 characters.",
            MinimumLength = 1)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required!")]
        [Display(Name = "Last name")]
        [StringLength(50, ErrorMessage = "Last name length must be between 1 and 50 characters.",
            MinimumLength = 6)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required!")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Enter correct data.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [Display(Name = "Password")]
        [StringLength(50, ErrorMessage = "Password length must be between 6 and 50 characters.",
            MinimumLength = 6)]
        [DataType(DataType.Password, ErrorMessage = "Enter correct data.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password confirmation is required!")]
        [Display(Name = "Password confirmation")]
        [DataType(DataType.Password, ErrorMessage = "Enter correct data.")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
