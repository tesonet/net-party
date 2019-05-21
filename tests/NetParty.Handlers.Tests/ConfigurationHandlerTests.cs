using FluentValidation;
using Moq;
using NetParty.Contracts.Requests;
using NetParty.Contracts.Requests.Validators;
using NetParty.Services.Interfaces;
using NUnit.Framework;
using System;

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
            Mock<ICredentialsService> credentialsServiceMock = new Mock<ICredentialsService>();

            var handler = new ConfigurationHandler(credentialsServiceMock.Object);
            Assert.ThrowsAsync<ArgumentNullException>(() => handler.HandleAsync(null));
        }


        [Test]
        public void RequiredFieldsTests()
        {
            Mock<ICredentialsService> credentialsServiceMock = new Mock<ICredentialsService>();

            var handler = new ConfigurationHandler(credentialsServiceMock.Object)
            {
                Validator = new ConfigurationRequestValidator()
            };

            ConfigurationRequest request = new ConfigurationRequest();
            Assert.ThrowsAsync<ValidationException>(() => handler.HandleAsync(request));
        }
    }
}
