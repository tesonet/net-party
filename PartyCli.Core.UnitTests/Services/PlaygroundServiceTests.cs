using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using Moq;
using PartyCli.Contracts.Exceptions;
using PartyCli.Contracts.Models;
using PartyCli.Core.Api;
using PartyCli.Core.Services;
using TestUtils;
using Xunit;

namespace PartyCli.Core.UnitTests.Services
{
	public class PlaygroundServiceTests
	{
		[Theory]
		[AutoMoqData]
		public async Task Throw_When_CredentialsAreEmpty(PlaygroundService service)
		{
			await Assert.ThrowsAsync<PartyCliException>(() => service.GetServers(new Config("", "")));
		}

		[Theory]
		[AutoMoqData]
		public async Task CallApiToGetToken([Frozen] Mock<IPlaygroundApiClient> apiClient, PlaygroundService service, Config config)
		{
			await service.GetServers(config);

			apiClient.Verify(a => a.GetToken(config.UserName, config.Password), Times.Once);
		}

		[Theory]
		[AutoMoqData]
		public async Task ReturnsServerList(
			[Frozen] Mock<IPlaygroundApiClient> apiClient,
			string token,
			List<Server> servers,
			PlaygroundService service,
			Config config)
		{
			apiClient.Setup(a => a.GetToken(config.UserName, config.Password)).ReturnsAsync(token);
			apiClient.Setup(a => a.GetServers(token)).ReturnsAsync(servers);

			var actual = await service.GetServers(config);

			Assert.True(servers.SequenceEqual(actual));
		}
	}
}