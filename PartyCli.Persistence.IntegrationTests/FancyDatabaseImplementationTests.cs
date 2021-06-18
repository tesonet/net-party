using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PartyCli.Contracts.Models;
using TestUtils;
using Xunit;

namespace PartyCli.Persistence.IntegrationTests
{
	public class FancyDatabaseImplementationTests
	{
		[Theory]
		[AutoMoqData]
		public async Task ShouldPersistConfig(Config config, FancyDatabaseImplementation database)
		{
			await database.Save(config);

			var persisted = await database.GetConfig();

			Assert.Equal(config, persisted);
		}

		[Theory]
		[AutoMoqData]
		public async Task ShouldPersistServerList(List<Server> servers, FancyDatabaseImplementation database)
		{
			await database.Save(servers);

			var persisted = await database.GetServers();

			Assert.True(servers.SequenceEqual(persisted));
		}
	}
}