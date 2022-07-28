using Microsoft.Extensions.DependencyInjection;
using StudentForum.DataAccess.Contracts.Account;
using StudentForum.DataAccess.Repositories;

namespace StudentForum.DataAccess.Extensions
{
    public static class RepositoryProviderExtension
    {
        public static void AddRepositories(this IServiceCollection serviceCollection)
            => serviceCollection
            .AddScoped<IAccountRepository, AccountRepository>();
    }
}
