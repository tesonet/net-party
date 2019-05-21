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
            Assert.Throws<ArgumentNullException>(() => new ConfigurationHandler(null));
        }

        [Test]
        public void RequestNullTests()
        {
            var credentialsServiceMock = new Mock<ICredentialsRepository>();

            var handler = new ConfigurationHandler(credentialsServiceMock.Object);
            Assert.ThrowsAsync<ArgumentNullException>(() => handler.HandleAsync(null));
        }


        [Test]
        public void RequiredFieldsTests()
        {
            var credentialsServiceMock = new Mock<ICredentialsRepository>();

            var handler = new ConfigurationHandler(credentialsServiceMock.Object)
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

            var handler = new ConfigurationHandler(credentialsServiceMock.Object);
            await handler.HandleBaseAsync(new ConfigurationRequest
            {
                UserName = userName,
                Password = testPassword
            });

            credentialsServiceMock.Verify(x => x.GetCredentialsAsync(), Times.Never);
            credentialsServiceMock.Verify(x => x.SaveCredentialsAsync(
                It.Is<Credentials>(c => c.Password == testPassword && c.UserName == userName)), 
                Times.Once);
        }
    }
}
