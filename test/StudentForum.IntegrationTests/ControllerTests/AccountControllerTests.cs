#nullable disable
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using StudentForum.IntegrationTests.Extensions;
using StudentForum.IntegrationTests.Helpers;

namespace StudentForum.IntegrationTests.ControllerTests
{
    public class AccountControllerTests
    {
        private HttpClient _httpClient;

        [OneTimeSetUp]
        public void Setup()
        {
            var factory = new WebApplicationFactory<Program>();
            var provider = new TestClaimsProvider();
            _httpClient = factory.CreateClientWithTestAuth(provider.WithAdminClaims());
        }

        [TestCase("register")]
        [TestCase("login")]
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
        public async Task GivenLogin_TryToLoginUser_ThenShouldBeRedirectStatusCode()
        {
            // Arrange
            var httpContent = new MultipartFormDataContent
            {
                { new StringContent("admin@test.com"), "email" },
                { new StringContent("Admin123@"), "password" },
            };

            // Act
            var response = await _httpClient.PostAsync("login", httpContent);

            // Assert
            Assert.AreEqual(HttpStatusCode.Redirect, response.StatusCode);
        }

        [Test]
        public async Task GivenLogin_TryToLoginUser_ThenShouldBeOkStatusCode()
        {
            // Arrange
            var httpContent = new MultipartFormDataContent
            {
                { new StringContent("admin@test.com"), "email" },
                { new StringContent("Admin123@1"), "password" },
            };

            // Act
            var response = await _httpClient.PostAsync("login", httpContent);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task GivenRegister_TryToRegisterUser_ThenShouldBeRedirectStatusCode()
        {
            // Arrange
            await using var stream = File.OpenRead(@"Helpers\Images\anonymous.jpg");

            var httpContent = new MultipartFormDataContent
            {
                { new StreamContent(stream), "CoverPhoto", "anonymous.jpg" },
                { new StringContent("Petr"), "FirstName" },
                { new StringContent("Petr"), "LastName" },
                { new StringContent($"{Guid.NewGuid()}admin@test.com"), "Email" },
                { new StringContent("Admin123@"), "Password" },
                { new StringContent("Admin123@"), "ConfirmPassword" },
            };

            // Act
            var response = await _httpClient.PostAsync("register", httpContent);

            // Assert
            Assert.AreEqual(HttpStatusCode.Redirect, response.StatusCode);
        }

        [Test]
        public async Task GivenRegister_TryToRegisterUserWithSameEmail_ThenShouldBeOkStatusCode()
        {
            // Arrange
            await using var stream = File.OpenRead(@"Helpers\Images\anonymous.jpg");

            var httpContent = new MultipartFormDataContent
            {
                { new StreamContent(stream), "CoverPhoto", "anonymous.jpg" },
                { new StringContent("Petr"), "FirstName" },
                { new StringContent("Petr"), "LastName" },
                { new StringContent($"admin@test.com"), "Email" },
                { new StringContent("Admin123@"), "Password" },
                { new StringContent("Admin123@"), "ConfirmPassword" },
            };

            // Act
            var response = await _httpClient.PostAsync("register", httpContent);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task GivenRegister_TryToRegisterUserWithoutFirstName_ThenShouldBeOKStatusCode()
        {
            // Arrange
            var httpContent = new MultipartFormDataContent
            {
                { new StringContent(""), "FirstName" },
                { new StringContent("Petr"), "LastName" },
                { new StringContent($"{Guid.NewGuid()}admin@test.com"), "Email" },
                { new StringContent("Admin123@"), "Password" },
                { new StringContent("Admin123@"), "ConfirmPassword" },
            };

            // Act
            var response = await _httpClient.PostAsync("register", httpContent);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task GivenRegister_TryToRegisterUserWithIncorrectPassword_ThenShouldBeOKStatusCode()
        {
            // Arrange
            var httpContent = new MultipartFormDataContent
            {
                { new StringContent("Petr"), "FirstName" },
                { new StringContent("Petr"), "LastName" },
                { new StringContent($"{Guid.NewGuid()}admin@test.com"), "Email" },
                { new StringContent("aaaaaaaa"), "Password" },
                { new StringContent("aaaaaaaa"), "ConfirmPassword" },
            };

            // Act
            var response = await _httpClient.PostAsync("register", httpContent);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task GivenLogout_TryToLogoutUser_ThenShouldBeRedirectStatusCode()
        {
            // Arrange
            var httpContent = new MultipartFormDataContent
            {
                { new StringContent("admin@test.com"), "email" },
                { new StringContent("Admin123@"), "password" },
            };
            await _httpClient.PostAsync("Account/Login", httpContent);

            // Act
            var response = await _httpClient.GetAsync("logout");

            // Assert
            Assert.AreEqual(HttpStatusCode.Redirect, response.StatusCode);
        }
    }
}
