using System.Threading.Tasks;
using Moq;
using NetParty.Contracts.Requests;
using NetParty.Contracts.Results;
using NetParty.Domain.Exceptions;
using NetParty.Domain.Interfaces;
using NetParty.Domain.Models;
using NetParty.Handlers;
using NUnit.Framework;

namespace NetParty.Tests.Handlers
{
    [TestFixture]
    public class WhenAuthorizeTests
    {
        private Mock<IServerRepository> _serverRepositoryMock;
        private Mock<ILocalConfigurationRepository> _localConfigurationRepositoryMock;

        private IRequestHandler<Authorize, AuthorizationResult> _whenAuthorize;

        [SetUp]
        public void SetUp()
        {
            _serverRepositoryMock = new Mock<IServerRepository>();
            _serverRepositoryMock.Setup(m => m.Authorize(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new AuthorizationData());

            _localConfigurationRepositoryMock = new Mock<ILocalConfigurationRepository>();
            _localConfigurationRepositoryMock.Setup(m => m.SaveAuthorizationData(It.IsAny<AuthorizationData>()));
        }

        [Test]
        public async Task Given_ValidUserCredentials_Then_AuthorizationDataIsSaved()
        {
            var authorizationRequest = new Authorize {Username = "username", Password = "password"};

            _whenAuthorize = new WhenAuthorize(_serverRepositoryMock.Object, _localConfigurationRepositoryMock.Object);
            var result = await _whenAuthorize.ThenAsync(authorizationRequest);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Message, Is.Not.Null);
            _serverRepositoryMock.Verify(m => m.Authorize(authorizationRequest.Username, authorizationRequest.Password), Times.Once);
            _localConfigurationRepositoryMock.Verify(m => m.SaveAuthorizationData(It.IsAny<AuthorizationData>()), Times.Once);
        }

        [Test]
        public void Given_InValidUserCredentials_Then_NotAuthorizedExceptionIsThrown()
        {
            var authorizationRequest = new Authorize {Username = "username", Password = "wrong_password"};
            _serverRepositoryMock.Setup(m => m.Authorize(It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(DomainExceptions.NotAuthorized);

            _whenAuthorize = new WhenAuthorize(_serverRepositoryMock.Object, _localConfigurationRepositoryMock.Object);

            Assert.That(async () => await _whenAuthorize.ThenAsync(authorizationRequest), Throws.Exception.TypeOf<DomainException>());
            _serverRepositoryMock.Verify(m => m.Authorize(authorizationRequest.Username, authorizationRequest.Password), Times.Once);
            _localConfigurationRepositoryMock.Verify(m => m.SaveAuthorizationData(It.IsAny<AuthorizationData>()), Times.Never);
        }
    }
}