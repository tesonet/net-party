using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using partycli.Contracts.Clients;
using partycli.Contracts.DTOs;
using partycli.Contracts.Entities;
using partycli.Contracts.Exceptions;
using partycli.Contracts.Repositories;
using partycli.Contracts.Services;

namespace partycli.Services
{
    public class ServerLedger: IServerLedger
    {
        private readonly IServerRepository _serverRepository;
        private readonly ILogger<ServerLedger> _logger;
        private readonly IServerListClient _serverListClient;
        private readonly IConfigurationRepository _configurationRepository;

        public ServerLedger(IServerRepository serverRepository, ILogger<ServerLedger> logger, IServerListClient serverListClient, IConfigurationRepository configurationRepository)
        {
            _serverRepository = serverRepository;
            _logger = logger;
            _serverListClient = serverListClient;
            _configurationRepository = configurationRepository;
        }

        public Task<IEnumerable<ServerDTO>> GetAllAsync(bool usePersistedData)
        {
            if (usePersistedData)
            {
                return GetFromLocalAsync();
            }

            return GetFromApiAsync();
        }

        private async Task<IEnumerable<ServerDTO>> GetFromLocalAsync()
        {
            _logger.LogDebug("Getting local server list.");

            var serverEntities = await _serverRepository.GetAllAsync();
            return serverEntities.Select(s => new ServerDTO(s.Name, s.Distance));
        }

        private async Task<IEnumerable<ServerDTO>> GetFromApiAsync()
        {
            _logger.LogDebug("Getting server list from Api.");
            var configuration = await _configurationRepository.GetAsync();

            if (configuration == null)
                throw new PartyException("Configuration is not set.");

            var token = await _serverListClient.GetTokenAsync(configuration.Username, configuration.Password);

            if (token == null)
                throw new PartyException("Token is missing.");

            var serverList = await _serverListClient.GetAllAsync(token);
            await _serverRepository.DeleteAllAsync();
            await _serverRepository.SaveAsync(serverList.Select(s => new ServerEntity
            {
                Name = s.Name,
                Distance = s.Distance
            }));

            return serverList;
        }

        public Task AddAsync(IList<ServerDTO> servers)
        {
            return _serverRepository.SaveAsync(servers.Select(s => new ServerEntity
            {
                Name = s.Name,
                Distance = s.Distance
            }));
        }
    }
}
