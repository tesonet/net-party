using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NetParty.Data;
using NetParty.Domain.Servers;
using NUnit.Framework;

namespace NetParty.Tests.Domain.Servers
{
    [TestFixture]
    internal class DefaultServerServiceTests
    {
        private Mock<IReadOnlyRepository<ICollection<Server>>> _sourceRepositoryMock;
        private Mock<IRepository<ICollection<Server>>> _defaultRepositoryMock;

        private DefaultServerService _service;

        [SetUp]
        public void SetUp()
        {
            _defaultRepositoryMock = new Mock<IRepository<ICollection<Server>>>();
            _sourceRepositoryMock = new Mock<IReadOnlyRepository<ICollection<Server>>>();
            _sourceRepositoryMock.Setup(_ => _.GetAsync()).ReturnsAsync(new List<Server>());
            _defaultRepositoryMock.Setup(_ => _.GetAsync()).ReturnsAsync(new List<Server>());

            _service = new DefaultServerService(_sourceRepositoryMock.Object, _defaultRepositoryMock.Object);
        }

        [Test]
        public async Task GetAll_ShouldFetchRemoteServersAndRefreshLocalStorage_If_refresh_IsTrue()
        {
            // act
            await _service.GetAll(true);

            // verify
            _sourceRepositoryMock.Verify(_ => _.GetAsync(), Times.Once);
            _defaultRepositoryMock.Verify(_ => _.SaveAsync(It.IsAny<ICollection<Server>>()), Times.Once);
            _defaultRepositoryMock.Verify(_ => _.GetAsync(), Times.Never);
        }

        [Test]
        public async Task GetAll_ShouldFetchFromLocalStorage_If_refresh_IsFalse()
        {
            // act
            await _service.GetAll(false);

            // verify
            _sourceRepositoryMock.Verify(_ => _.GetAsync(), Times.Never);
            _defaultRepositoryMock.Verify(_ => _.GetAsync(), Times.Once);
        }
    }
}
