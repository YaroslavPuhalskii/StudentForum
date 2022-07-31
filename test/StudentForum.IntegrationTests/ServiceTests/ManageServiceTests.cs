#nullable disable
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using StudentForum.BusinessLogic.Abstractions;
using StudentForum.BusinessLogic.Services;
using StudentForum.IntegrationTests.Extensions;
using System;

namespace StudentForum.IntegrationTests.ServiceTests
{
    public class ManageServiceTests
    {
        private IManageService _manageService;

        [SetUp]
        public void Setup()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddExtensions();
            var provider = serviceCollection.BuildServiceProvider();
            _manageService = provider.GetService<ManageService>();
        }

        [Test]
        public void GivenGetUserById_TryToGetWithoutId_ThenShouldBeArgumentNullException()
        {
            // Act and Assert
            Assert.ThrowsAsync<ArgumentNullException>(
                () => _manageService.GetUserById(null));
        }

        [Test]
        public void GivenUpdatePhoto_TryToUpdateWithoutPhoto_ThenShouldBeArgumentOutOfRangeException()
        {
            // Act and Assert
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(
                () => _manageService.UdpatePhoto(null));
        }

        [Test]
        public void GivenUpdate_TryToUpdateWithoutUserInfo_ShouldBeArgumentNullException()
        {
            // Arrange and Act
            Assert.ThrowsAsync<ArgumentNullException>(
                () => _manageService.Update(null));
        }

        [Test]
        public void GivenChangePassword_TryToChangeWithIncorrectData_ShouldBeArgumentNullException()
        {
            // Arrange and Act
            Assert.ThrowsAsync<ArgumentNullException>(
                () => _manageService.ChangePassword(null));
        }
    }
}
