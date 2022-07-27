using Microsoft.Extensions.DependencyInjection;
using StudentForum.BusinessLogic.Abstractions;
using StudentForum.BusinessLogic.Services;
using StudentForum.DataAccess.Extensions;

namespace StudentForum.BusinessLogic.Extensions
{
    public static class ServiceProviderExtension
    {
        public static void AddServices(this IServiceCollection serviceCollection)
            => serviceCollection
            .AddTransient<IAccountService, AccountService>()
            .AddRepositories();
    }
}
