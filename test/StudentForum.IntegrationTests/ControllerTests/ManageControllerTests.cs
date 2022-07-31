#nullable disable
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using StudentForum.IntegrationTests.Extensions;
using StudentForum.IntegrationTests.Helpers;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace StudentForum.IntegrationTests.ControllerTests
{
    public class ManageControllerTests
    {
        private HttpClient _httpClient;

        [OneTimeSetUp]
        public void Setup()
        {
            var factory = new WebApplicationFactory<Program>();
            var provider = new TestClaimsProvider();
            _httpClient = factory.CreateClientWithTestAuth(provider.WithAdminClaims());
        }

        [TestCase("profile")]
        [TestCase("update-photo")]
        [TestCase("Manage/Load")]
        [TestCase("Manage/Update")]
        public async Task GetEndpoints_TryToReturnContent_ThenShouldBeReturnedCorrectContentType(string url)
        {
            // Act
            var response = await _httpClient.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Test]
        public async Task GivenUpdatePhoto_TryToUpdateWithoutPhoto_ShouldBeOkResponse()
        {
            // Arrange
            var httpContent = new MultipartFormDataContent();

            // Act
            var response = await _httpClient.PostAsync("Manage/UpdatePhoto", httpContent);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task GivenUpdatePhoto_TryToUpdatePhoto_ShouldBeOkRepsponse()
        {
            // Arrange
            await using var stream = File.OpenRead(@"Helpers\Images\anonymous.jpg");

            var httpContent = new MultipartFormDataContent
            {
                { new StreamContent(stream), "Photo", "anonymous.jpg" },
            };

            // Act
            var response = await _httpClient.PostAsync("Manage/UpdatePhoto", httpContent);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task GivenUpdate_TryToUpdateWithoutEmail_ThenShouldBeOkStatusCode()
        {
            // Arrange
            var httpContent = new MultipartFormDataContent
            {
                { new StringContent("Petr"), "FirstName" },
                { new StringContent("Petr"), "LastName" },
            };

            // Act
            var response = await _httpClient.PostAsync("Manage/Update", httpContent);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task GivenUpdate_TryToUpdateUser_ThenShouldBeOkStatusCode()
        {
            // Arrange
            var httpContent = new MultipartFormDataContent
            {
                { new StringContent("Petr"), "FirstName" },
                { new StringContent("Petr"), "LastName" },
                { new StringContent("admin@test.com"), "Email"},
            };

            // Act
            var response = await _httpClient.PostAsync("Manage/Update", httpContent);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
