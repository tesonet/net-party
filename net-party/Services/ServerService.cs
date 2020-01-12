using Microsoft.Extensions.DependencyInjection;
using net_party.Entities.API;
using net_party.Entities.Database;
using net_party.Repositories.Contracts;
using net_party.Services.Base;
using net_party.Services.Contracts;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace net_party.Services
{
    public class ServerService : BaseRestService, IServerService
    {
        private readonly IServerRepository _serverRepository;
        private readonly IAuthTokenRepository _authTokenRepository;

        private const string SERVER_LIST = "/servers";

        public ServerService(IServiceProvider services) : base(services)
        {
            _serverRepository = services.GetService<IServerRepository>();
            _authTokenRepository = services.GetService<IAuthTokenRepository>();
        }

        public async Task<IEnumerable<Server>> GetServers(bool local = false)
        {
            if (local)
                return await GetServersFromRepository();
            else
                return await GetServersFromApi();
        }

        private async Task<IEnumerable<Server>> GetServersFromRepository() => await _serverRepository.Get();

        private async Task<IEnumerable<Server>> GetServersFromApi()
        {
            var token = await _authTokenRepository.Get();

            var request = BaseRequest(SERVER_LIST, Method.GET);
            request.AddHeader("Authorization", $"Bearer {token.Token}");
            request.AddHeader("Content-Type", "application/json");

            var response = await ExecuteManyAsync<ServerListResponse>(request);
            var servers = TransformFromResponse(response);
            await _serverRepository.AddMany(servers);

            return servers;
        }

        private IEnumerable<Server> TransformFromResponse(IEnumerable<ServerListResponse> serverResponse)
        {
            foreach (var item in serverResponse)
            {
                yield return Server.FromResponse(item);
            }
        }
    }
}