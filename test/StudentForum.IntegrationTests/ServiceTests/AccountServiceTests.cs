#nullable disable
using System;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using StudentForum.BusinessLogic.Abstractions;
using StudentForum.BusinessLogic.Services;
using StudentForum.IntegrationTests.Extensions;

namespace StudentForum.IntegrationTests.ServiceTests
{
    public class AccountServiceTests
    {
        private IAccountService _accountService;

        [SetUp]
        public void Setup()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddExtensions();
            var provider = serviceCollection.BuildServiceProvider();
            _accountService = provider.GetService<AccountService>();
        }

        [Test]
        public void GivenLogin_TryToLoginWithNullModel_ThenShouldBeArgumentNullException()
        {
            // Act and Assert
            Assert.ThrowsAsync<ArgumentNullException>(
                () => _accountService.Login(null));
        }

        [Test]
        public void GivenRegister_TryToRegisterWithNullModel_ThenShouldBeArgumentNullException()
        {
            // Act and Assert
            Assert.ThrowsAsync<ArgumentNullException>(
                () => _accountService.Register(null));
        }
    }
}
