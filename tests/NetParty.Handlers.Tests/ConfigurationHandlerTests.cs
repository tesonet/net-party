using FluentValidation;
using Moq;
using NetParty.Contracts.Requests;
using NetParty.Contracts.Requests.Validators;
using NetParty.Services.Interfaces;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using NetParty.Contracts;
using NetParty.Repositories.Core;

namespace NetParty.Handlers.Tests
{
    [TestFixture]
    public class ConfigurationHandlerTests
    {
        [Test]
        public void ConstructorTest()
        {
            Assert.Throws<ArgumentNullException>(() => new ConfigurationHandler(null, null));
            Assert.Throws<ArgumentNullException>(() => new ConfigurationHandler(new Mock<ICredentialsRepository>().Object, null));
        }

        [Test]
        public void RequestNullTests()
        {
            var credentialsServiceMock = new Mock<ICredentialsRepository>();

            var handler = new ConfigurationHandler(credentialsServiceMock.Object, new Mock<IDisplayService>().Object);
            Assert.ThrowsAsync<ArgumentNullException>(() => handler.HandleAsync(null));
        }


        [Test]
        public void RequiredFieldsTests()
        {
            var credentialsServiceMock = new Mock<ICredentialsRepository>();

            var handler = new ConfigurationHandler(credentialsServiceMock.Object, new Mock<IDisplayService>().Object)
            {
                Validator = new ConfigurationRequestValidator()
            };

            ConfigurationRequest request = new ConfigurationRequest();
            Assert.ThrowsAsync<ValidationException>(() => handler.HandleAsync(request));
        }

        [Test]
        public async Task SaveCredentialsTest()
        {
            var userName = "TestUser";
            var testPassword = "TestPassword";

            var credentialsServiceMock = new Mock<ICredentialsRepository>();
            var displayServiceMock = new Mock<IDisplayService>();

            var handler = new ConfigurationHandler(credentialsServiceMock.Object, displayServiceMock.Object);
            await handler.HandleBaseAsync(new ConfigurationRequest
            {
                UserName = userName,
                Password = testPassword
            }).ConfigureAwait(false);

            credentialsServiceMock.Verify(x => x.GetCredentialsAsync(), Times.Never);
            credentialsServiceMock.Verify(x => x.SaveCredentialsAsync(
                It.Is<Credentials>(c => c.Password == testPassword && c.UserName == userName)), 
                Times.Once);
            displayServiceMock.Verify(x => x.DisplayText("Welcome to NetParty. Let's go to see servers list 'NetParty.Application.exe server_list'!"), Times.Once);
        }
    }
}
