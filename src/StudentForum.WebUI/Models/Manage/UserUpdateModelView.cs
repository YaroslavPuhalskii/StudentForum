#nullable disable
using System.ComponentModel.DataAnnotations;

namespace StudentForum.WebUI.Models.Manage
{
    public class UserUpdateModelView
    {
        [Required(ErrorMessage = "First name is required!")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required!")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required!")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
