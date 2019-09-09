using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NetPartyCli.Database;
using NetPartyCli.Dto;
using NetPartyCli.Repositories;

namespace NetPartyCli.Services
{
    public class ServerService
    {
        private ServerRepository _serverRepository;
        private TesonetClient _tesonetClient;
        private ILogger<ServerService> _logger;
        private UserRepository _userRepository;

        public ServerService(ServerRepository serverRepository, ILogger<ServerService> logger,
            TesonetClient tesonetClient, UserRepository userRepository)
        {
            _serverRepository = serverRepository;
            _logger = logger;
            _tesonetClient = tesonetClient;
            _userRepository = userRepository;
        }

        public Task<IEnumerable<ServerDto>> GetAllAsync(bool usePersistedData)
        {
            return usePersistedData ? GetFromLocalAsync() : GetFromApiAsync();
        }

        private async Task<IEnumerable<ServerDto>> GetFromLocalAsync()
        {
            _logger.LogInformation("Getting local server list.");
            var localServers = await _serverRepository.GetAllAsync();
            return localServers.Select(s => new ServerDto(s.Name, s.Distance));
        }

        private async Task<IEnumerable<ServerDto>> GetFromApiAsync()
        {
            _logger.LogInformation("Getting server list from Api.");
            var user = await _userRepository.GetAsync();

            if (user == null)
                throw new System.Exception("User is not set.");

            var token = await _tesonetClient.GetTokenAsync(user.Username, user.Password);

            if (token == null)
                throw new System.Exception("Token is missing.");

            var serverList = await _tesonetClient.GetAllAsync(token);
            await _serverRepository.DeleteAllAsync();
            await _serverRepository.SaveAsync(serverList.Select(s => new Server
            {
                Name = s.Name,
                Distance = s.Distance
            }));

            return serverList;
        }

        public Task AddAsync(IList<ServerDto> servers)
        {
            return _serverRepository.SaveAsync(servers.Select(s => new Server
            {
                Name = s.Name,
                Distance = s.Distance
            }));
        }
    }
}
