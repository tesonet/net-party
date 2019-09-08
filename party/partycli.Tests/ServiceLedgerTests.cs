using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using partycli.Contracts.Clients;
using partycli.Contracts.DTOs;
using partycli.Contracts.Entities;
using partycli.Contracts.Repositories;
using partycli.Services;
using Xunit;

namespace partycli.Tests
{
    public class ServiceLedgerTests
    {
        private readonly ServerLedger _serverLedger;
        private readonly Fixture _fixture;

        private readonly Mock<IServerRepository> _serverRepMock;
        private readonly Mock<IServerListClient> _apiClientMock;
        private readonly Mock<IConfigurationRepository> _configRepMock;

        public ServiceLedgerTests()
        {
            _fixture = new Fixture();

            _serverRepMock = new Mock<IServerRepository>(MockBehavior.Strict);
            _apiClientMock = new Mock<IServerListClient>(MockBehavior.Strict);
            _configRepMock = new Mock<IConfigurationRepository>(MockBehavior.Strict);

            _serverLedger = new ServerLedger(_serverRepMock.Object, new NullLogger<ServerLedger>(), _apiClientMock.Object, _configRepMock.Object);
        }

        [Fact]
        public async Task ShouldReturnLocalServers()
        {
            var servers = _fixture.Create<List<ServerEntity>>();

            _serverRepMock.Setup(s => s.GetAllAsync()).ReturnsAsync(servers);

            var result = await _serverLedger.GetAllAsync(true);

            result.AssertWith(servers, (e, a) =>
            {
                Assert.Equal(e.Distance, a.Distance);
                Assert.Equal(e.Name, a.Name);
            });

            _serverRepMock.Verify(v => v.GetAllAsync(), Times.Once);
            _apiClientMock.Verify(v => v.GetAllAsync(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task ShouldGetServerFromApi()
        {
            var servers = _fixture.Create<List<ServerDTO>>();
            var config = _fixture.Create<ConfigurationEntity>();
            var token = _fixture.Create<string>();

            _configRepMock.Setup(s => s.GetAsync()).ReturnsAsync(config);
            _apiClientMock.Setup(s => s.GetTokenAsync(config.Username, config.Password)).ReturnsAsync(token);
            _apiClientMock.Setup(s => s.GetAllAsync(token)).ReturnsAsync(servers);
            _serverRepMock.Setup(s => s.DeleteAllAsync()).Returns(Task.CompletedTask);
            _serverRepMock.Setup(s => s.SaveAsync(It.IsAny<IEnumerable<ServerEntity>>())).Returns(Task.CompletedTask);

            var result = await _serverLedger.GetAllAsync(false);

            result.AssertWith(servers, (e, a) =>
            {
                Assert.Equal(e.Distance, a.Distance);
                Assert.Equal(e.Name, a.Name);
            });

            _serverRepMock.Verify(v => v.GetAllAsync(), Times.Never);
            _apiClientMock.Verify(v => v.GetAllAsync(It.IsAny<string>()), Times.Once);
            _apiClientMock.Verify(v => v.GetTokenAsync(config.Username, config.Password), Times.Once);
            _serverRepMock.Verify(v => v.DeleteAllAsync(), Times.Once);
            _serverRepMock.Verify(v => v.SaveAsync(It.IsAny<IEnumerable<ServerEntity>>()), Times.Once);
        }
    }
}
