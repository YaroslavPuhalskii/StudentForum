using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StudentForum.BusinessLogic.Extensions;
using StudentForum.BusinessLogic.Services;
using StudentForum.Data.Entities.Account;
using StudentForum.DataAccess;
using StudentForum.WebUI.MapperConfig;
using BLConfig = StudentForum.BusinessLogic.MapperConfig;

namespace StudentForum.IntegrationTests.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddExtensions(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddAutoMapper(typeof(AccountMapperProfile).Assembly,
                typeof(BLConfig.MapperProfile).Assembly);
            serviceCollection.AddDbContext<SFDatabaseContext>(
                options => options.UseSqlServer(TestDb.ConnectionString));
            serviceCollection.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<SFDatabaseContext>()
                .AddDefaultTokenProviders();
            serviceCollection.Configure<IdentityOptions>(option =>
            {
                option.User.RequireUniqueEmail = true;
            });

            serviceCollection.AddLogging();

            serviceCollection.AddServices();

            serviceCollection.AddTransient<AccountService>();
            serviceCollection.AddTransient<ManageService>();

            return serviceCollection;
        }
    }
}
