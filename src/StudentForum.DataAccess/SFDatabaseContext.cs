using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentForum.Data.Entities.Account;

namespace StudentForum.DataAccess
{
    public class SFDatabaseContext : IdentityDbContext<User>
    {
        public SFDatabaseContext(DbContextOptions<SFDatabaseContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
