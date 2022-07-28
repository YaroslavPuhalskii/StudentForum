using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StudentForum.DataAccess;
using StudentForum.IntegrationTests.Helpers;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace StudentForum.IntegrationTests.Extensions
{
    internal static class WebApplicationFactoryExtensions
    {
        internal static WebApplicationFactory<T> WithAuthentication<T>(this WebApplicationFactory<T> factory, TestClaimsProvider claimsProvider)
            where T : class
        {
            return factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType ==
                        typeof(DbContextOptions<SFDatabaseContext>));

                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }

                    services.AddDbContext<SFDatabaseContext>(options =>
                    {
                        options.UseSqlServer(TestDb.ConnectionString);
                    });

                    services.AddAuthentication(o =>
                    {
                        o.DefaultAuthenticateScheme = "EventManager";
                        o.DefaultChallengeScheme = "EventManager";
                    }).AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("EventManager", op => { });

                    services.AddScoped(_ => claimsProvider);
                });
            });
        }

        internal static HttpClient CreateClientWithTestAuth<T>(this WebApplicationFactory<T> factory,
            TestClaimsProvider claimsProvider)
            where T : class
        {
            var client = factory.WithAuthentication(claimsProvider).CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false,
            });

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("EventManager");

            return client;
        }
    }
}
