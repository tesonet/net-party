#region Using

using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using NetParty.Application.IntegrationTests.Common.Helpers;
using NetParty.Application.Servers;
using NUnit.Framework;

#endregion

namespace NetParty.Application.IntegrationTests.Servers.LocalServerRepository
    {
    [TestFixture]
    internal class GetServersAsyncTests
        {
        [Test]
        [AutoStorageData]
        public async Task ServersAreStored_ServersAreLoaded(
            [Frozen] IServerStorageProvider storageProvider,
            Application.Servers.LocalServerRepository localServerRepository)
            {
            // arrange
            var storedServers = new[] {new Server("ServerA", 100), new Server("ServerB", 200)};
            await localServerRepository.StoreServersAsync(storedServers);

            // act
            var loadedServers = await localServerRepository.GetServersAsync();

            // assert
            loadedServers.Should().Equal(storedServers);
            }
        }
    }
