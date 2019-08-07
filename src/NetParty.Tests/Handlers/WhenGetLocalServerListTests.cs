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
    public class WhenGetLocalServerListTests
    {
        private Mock<ILocalServerRepository> _localServerRepositoryMock;

        private IRequestHandler<GetLocalServerList, ServerList> _whenGetLocalServerList;

        [SetUp]
        public void SetUp()
        {
            _localServerRepositoryMock = new Mock<ILocalServerRepository>();
            _localServerRepositoryMock.Setup(m => m.Get()).ReturnsAsync(new List<Server>());
        }

        [Test]
        public async Task Given_AuthorizedUser_Then_ServerListIsReturned()
        {
            var request = new GetLocalServerList();

            _whenGetLocalServerList = new WhenGetLocalServerList(_localServerRepositoryMock.Object);
            var result = await _whenGetLocalServerList.ThenAsync(request);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Items, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(result.Items.Count));
            _localServerRepositoryMock.Verify(m => m.Get(), Times.Once);
        }

        [Test]
        public void Given_NoLocalServersPersisted_Then_NoLocalServersFoundExceptionIsThrown()
        {
            var request = new GetLocalServerList();
            _localServerRepositoryMock.Setup(m => m.Get()).ThrowsAsync(DomainExceptions.NoLocalServersFound);

            _whenGetLocalServerList = new WhenGetLocalServerList(_localServerRepositoryMock.Object);

            Assert.That(async () => await _whenGetLocalServerList.ThenAsync(request), Throws.Exception.TypeOf<DomainException>());
        }

        [Test]
        public void Given_LocalServerRepositoryReturnsNull_Then_CouldNotRetrieveLocalServersExceptionIsThrown()
        {
            var request = new GetLocalServerList();
            _localServerRepositoryMock.Setup(m => m.Get()).ReturnsAsync((IEnumerable<Server>) null);

            _whenGetLocalServerList = new WhenGetLocalServerList(_localServerRepositoryMock.Object);

            Assert.That(async () => await _whenGetLocalServerList.ThenAsync(request), Throws.Exception.TypeOf<DomainException>());
        }
    }
}