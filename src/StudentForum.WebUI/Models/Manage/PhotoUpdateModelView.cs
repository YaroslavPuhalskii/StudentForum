#nullable disable
using System.ComponentModel.DataAnnotations;

namespace StudentForum.WebUI.Models.Manage
{
    public class PhotoUpdateModelView
    {
        [Required(ErrorMessage = "Photo is required!")]
        [Display(Name = "Photo")]
        public IFormFile Photo { get; set; }
    }
}
