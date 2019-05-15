using System.Collections.Generic;
using System.Threading.Tasks;
using NetParty.Contracts;
using NetParty.Contracts.Exceptions;
using NetParty.Interfaces.Repositories;
using NetParty.Interfaces.Services;
using NetParty.Interfaces.Clients;

namespace NetParty.Services
{
    public class ServerListService : IServerListService
    {
        private readonly ICredentialsRepository _credentialsRepository;
        private readonly IAuthorizationClient _authorizationClient;
        private readonly IServersClient _serversClient;
        private readonly IServersRepository _serversRepository;
        private readonly IOutputService _outputService;

        public ServerListService(ICredentialsRepository credentialsRepository,
            IAuthorizationClient authorizationClient,
            IServersClient serversClient,
            IServersRepository serversRepository,
            IOutputService outputService)
        {
            _credentialsRepository = credentialsRepository;
            _authorizationClient = authorizationClient;
            _serversClient = serversClient;
            _serversRepository = serversRepository;
            _outputService = outputService;
        }

        public async Task PrintServerList(bool local)
        {
            List<Server> servers;
            if (local)
            {
                servers = await _serversRepository.GetServers();
                await _outputService.OutputStringLine("Servers LOADED locally");
            }
            else
            {
                Credentials credentials = await _credentialsRepository.LoadCredentials();
                if (credentials == null)
                    throw new BaseValidationException("No saved credentials were found!");
                var token = await _authorizationClient.FetchToken(credentials);
                if(token == null)
                    throw new BaseValidationException("Failed to fetch token with saved credentials!");
                servers = await _serversClient.FetchServers(token);
                await _serversRepository.SaveServers(servers);
                await _outputService.OutputStringLine("Servers SAVED locally");
            }

            await _outputService.OutputServers(servers);
        }
    }
}