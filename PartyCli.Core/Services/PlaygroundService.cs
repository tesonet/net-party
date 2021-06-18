using System.Collections.Generic;
using System.Threading.Tasks;
using PartyCli.Contracts.Exceptions;
using PartyCli.Contracts.Models;
using PartyCli.Core.Api;

namespace PartyCli.Core.Services
{
	public class PlaygroundService : IPlaygroundService
	{
		private readonly IPlaygroundApiClient _apiClient;

		public PlaygroundService(IPlaygroundApiClient apiClient)
		{
			_apiClient = apiClient;
		}

		public async Task<IEnumerable<Server>> GetServers(Config config)
		{
			if (string.IsNullOrWhiteSpace(config.Password) || string.IsNullOrWhiteSpace(config.Password))
			{
				throw new PartyCliException("Invalid or missing credentials");
			}

			var token = await _apiClient.GetToken(config.UserName, config.Password);
			return await _apiClient.GetServers(token);
		}
	}
}