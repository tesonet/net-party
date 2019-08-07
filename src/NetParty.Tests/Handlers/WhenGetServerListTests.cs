using System.Collections.Generic;
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
    public class WhenGetServerListTests
    {
        private Mock<IServerRepository> _serverRepositoryMock;
        private Mock<ILocalServerRepository> _localServerRepositoryMock;

        private IRequestHandler<GetServerList, ServerList> _whenGetServerList;

        [SetUp]
        public void SetUp()
        {
            _serverRepositoryMock = new Mock<IServerRepository>();
            _serverRepositoryMock.Setup(m => m.Get()).ReturnsAsync(new List<Server>());

            _localServerRepositoryMock = new Mock<ILocalServerRepository>();
            _localServerRepositoryMock.Setup(m => m.Save(It.IsAny<IEnumerable<Server>>()));
        }

        [Test]
        public async Task Given_AuthorizedUser_Then_ServerListIsReturned()
        {
            var request = new GetServerList();

            _whenGetServerList = new WhenGetServerList(_serverRepositoryMock.Object, _localServerRepositoryMock.Object);
            var result = await _whenGetServerList.ThenAsync(request);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Items, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(result.Items.Count));
            _serverRepositoryMock.Verify(m => m.Get(), Times.Once);
            _localServerRepositoryMock.Verify(m => m.Save(It.IsAny<IEnumerable<Server>>()), Times.Once);
        }

        [Test]
        public void Given_UnAuthorizedUser_Then_NotAuthorizedExceptionIsThrown()
        {
            var request = new GetServerList();
            _serverRepositoryMock.Setup(m => m.Get()).ThrowsAsync(DomainExceptions.NotAuthorized);

            _whenGetServerList = new WhenGetServerList(_serverRepositoryMock.Object, _localServerRepositoryMock.Object);

            Assert.That(async () => await _whenGetServerList.ThenAsync(request), Throws.Exception.TypeOf<DomainException>());
            _serverRepositoryMock.Verify(m => m.Get(), Times.Once);
            _localServerRepositoryMock.Verify(m => m.Save(It.IsAny<IEnumerable<Server>>()), Times.Never);
        }
    }
}