#nullable disable
using Microsoft.Extensions.Configuration;

namespace StudentForum.IntegrationTests
{
    internal static class TestDb
    {
        private static string _connectionString;

        internal static string ConnectionString => _connectionString ??= GetConnectionString();

        private static string GetConnectionString()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
                .GetConnectionString("Database");
        }
    }
}
