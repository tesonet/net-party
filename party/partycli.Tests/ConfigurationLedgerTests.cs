using System.Threading.Tasks;
using AutoFixture;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using partycli.Contracts.DTOs;
using partycli.Contracts.Entities;
using partycli.Contracts.Repositories;
using partycli.Services;
using Xunit;

namespace partycli.Tests
{
    public class ConfigurationLedgerTests
    {
        private readonly ConfigurationLedger _configurationLedger;
        private readonly Fixture _fixture;

        private readonly Mock<IConfigurationRepository> _configRepMock;

        public ConfigurationLedgerTests()
        {
            _fixture = new Fixture();

            _configRepMock = new Mock<IConfigurationRepository>(MockBehavior.Strict);

            _configurationLedger = new ConfigurationLedger(_configRepMock.Object, new NullLogger<ConfigurationLedger>());
        }

        [Fact]
        public async Task ShouldAddNewConfiguration()
        {
            var config = _fixture.Create<ConfigurationDTO>();

            _configRepMock.Setup(s => s.GetAsync()).ReturnsAsync((ConfigurationEntity)null);
            _configRepMock.Setup(s => s.SaveAsync(
                It.Is<ConfigurationEntity>(x => x.Username == config.Username && x.Password == config.Password)))
                .Returns(Task.CompletedTask);

            await _configurationLedger.AddOrUpdateAsync(config);

            _configRepMock.Verify(v => v.SaveAsync(It.IsAny<ConfigurationEntity>()), Times.Once());
        }

        [Fact]
        public async Task ShouldUpdateConfiguration()
        {
            var config = _fixture.Create<ConfigurationEntity>();
            var newConfig = _fixture.Create<ConfigurationDTO>();

            _configRepMock.Setup(s => s.GetAsync()).ReturnsAsync(config);
            _configRepMock.Setup(s => s.UpdateAsync(
                    It.Is<ConfigurationEntity>(x => x.Username == newConfig.Username && x.Password == newConfig.Password)))
                .Returns(Task.CompletedTask);

            await _configurationLedger.AddOrUpdateAsync(newConfig);

            _configRepMock.Verify(v => v.SaveAsync(It.IsAny<ConfigurationEntity>()), Times.Never);
            _configRepMock.Verify(v => v.UpdateAsync(It.IsAny<ConfigurationEntity>()), Times.Once);

        }
    }
}
