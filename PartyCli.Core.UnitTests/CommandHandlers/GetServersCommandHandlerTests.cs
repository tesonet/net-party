using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using Moq;
using PartyCli.Contracts.Models;
using PartyCli.Core.CommandHandlers;
using PartyCli.Core.Commands;
using PartyCli.Core.Services;
using PartyCli.Persistence;
using TestUtils;
using Xunit;

namespace PartyCli.Core.UnitTests.CommandHandlers
{
	public class GetServerListCommandHandlerTests
	{
		[Theory]
		[AutoMoqData]
		public async Task ReturnListFromDatabase_When_UseLocalParameterIsProvided(
			[Frozen] Mock<IPlaygroundService> playgroundService,
			[Frozen] Mock<IServerRepository> serverRepository,
			GetServerListCommand command, GetServerListCommandHandler handler)
		{
			command = command with { UseLocal = true };

			await handler.Handle(command, CancellationToken.None);

			serverRepository.Verify(r => r.GetServers(), Times.Once);
			playgroundService.Verify(r => r.GetServers(It.IsAny<Config>()), Times.Never);
		}

		[Theory]
		[AutoMoqData]
		public async Task ReturnListFromService_When_UseLocalParameterIsNotProvided(
			[Frozen] Mock<IPlaygroundService> playgroundService,
			[Frozen] Mock<IServerRepository> serverRepository,
			GetServerListCommand command, GetServerListCommandHandler handler)
		{
			command = command with { UseLocal = false };

			await handler.Handle(command, CancellationToken.None);

			serverRepository.Verify(r => r.GetServers(), Times.Never);
			playgroundService.Verify(r => r.GetServers(It.IsAny<Config>()), Times.Once);
		}

		[Theory]
		[AutoMoqData]
		public async Task SaveServerListToDb_When_UseLocalParameterIsNotProvided(
			[Frozen] Mock<IServerRepository> serverRepository,
			GetServerListCommand command, GetServerListCommandHandler handler)
		{
			command = command with { UseLocal = false };

			await handler.Handle(command, CancellationToken.None);

			serverRepository.Verify(r => r.Save(It.IsAny<IEnumerable<Server>>()), Times.Once);
		}

		[Theory]
		[AutoMoqData]
		public async Task QuerySavedConfigFromDb_When_UseLocalParameterIsNotProvided(
			[Frozen] Mock<IConfigRepository> configRepository,
			GetServerListCommand command, GetServerListCommandHandler handler)
		{
			command = command with { UseLocal = false };

			await handler.Handle(command, CancellationToken.None);

			configRepository.Verify(r => r.GetConfig(), Times.Once);
		}
	}
}