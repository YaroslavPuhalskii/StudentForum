#nullable disable
namespace StudentForum.BusinessLogic.Models.Manage
{
    public class ChangePasswordDto
    {
        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }
    }
}
