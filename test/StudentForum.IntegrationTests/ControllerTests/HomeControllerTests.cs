#nullable disable
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using StudentForum.IntegrationTests.Extensions;
using StudentForum.IntegrationTests.Helpers;
using System.Net.Http;
using System.Threading.Tasks;

namespace StudentForum.IntegrationTests.ControllerTests
{
    public class HomeControllerTests
    {
        private HttpClient _httpClient;

        [OneTimeSetUp]
        public void Setup()
        {
            var factory = new WebApplicationFactory<Program>();
            var provider = new TestClaimsProvider();
            _httpClient = factory.CreateClientWithTestAuth(provider.WithUserClaims());
        }

        [Test]
        public async Task GivenIndex_TryToGetEndpoints_ThenShouldBeReturnedCorrectContentType()
        {
            // Act
            var response = await _httpClient.GetAsync("Home/Index");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }
    }
}
