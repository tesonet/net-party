using Moq;
using NetParty.Contracts.Requests;
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
        public void RequiredFielsTests()
        {
            Mock<ICredentialsService> credentialsServiceMock = new Mock<ICredentialsService>();

            var handler = new ConfigurationHandler(credentialsServiceMock.Object);

            ConfigurationRequest request = new ConfigurationRequest();
            Assert.ThrowsAsync<ArgumentNullException>(() => handler.HandleAsync(request));
        }
    }
}
