#nullable disable
using Microsoft.AspNetCore.Identity;

namespace StudentForum.Data.Entities.Account
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
