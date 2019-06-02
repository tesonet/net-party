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
    public class StoreServersAsyncTests
        {
        [Test]
        [AutoStorageData]
        public async Task ServersAreStored_StorageIsNotEmpty(
            [Frozen] IServerStorageProvider storageProvider,
            Application.Servers.LocalServerRepository localServerRepository)
            {
            // arrange
            var servers = new[] {new Server("ServerA", 100), new Server("ServerB", 200)};

            // act
            await localServerRepository.StoreServersAsync(servers);

            // assert
            storageProvider.GetStorage().Length.Should().BeGreaterThan(0, "servers should have been stored");
            }
        }
    }
