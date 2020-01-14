using Microsoft.Extensions.DependencyInjection;
using net_party.Entities.API;
using net_party.Entities.Database;
using net_party.Repositories.Contracts;
using net_party.Services.Base;
using net_party.Services.Contracts;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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

            if (token == null)
            {
                Console.WriteLine("No credentials found. Please authenticate first using the \"config\" command");
                return Enumerable.Empty<Server>();
            }

            var response = await GetServers(token);
            var servers = TransformFromResponse(response);

            if (response == null)
            {
                Console.WriteLine("Attempt to get the remote server list failed.");

                return Enumerable.Empty<Server>();
            }

            await StoreServersToStorage(servers);

            return servers;
        }

        private async Task StoreServersToStorage(IEnumerable<Server> servers)
        {
            var connection = _services.GetService<SqlConnection>();
            await connection.OpenAsync();

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    await _serverRepository.Truncate();
                    await _serverRepository.AddMany(servers);

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
                finally
                {
                    transaction.Dispose();
                }
            }
        }

        private async Task<IEnumerable<ServerListResponse>> GetServers(AuthToken token)
        {
            var request = BaseRequest(SERVER_LIST, Method.GET);
            request.AddHeader("Authorization", $"Bearer {token.Token}");
            request.AddHeader("Content-Type", "application/json");
            var response = await ExecuteManyAsync<ServerListResponse>(request);

            return response;
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