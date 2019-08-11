using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Net_party.Logging;
using Net_party.Repositories.Server;
using Net_party.Services.Config;
using Net_party.Services.Credentials;
using Newtonsoft.Json;

namespace Net_party.Services.Server
{
    class ServerService : IServerService
    {
        private readonly ServerRepository _serverRepository;
        private readonly ICredentialsService _credentialsService;

        public ServerService()
        {
            _serverRepository = new ServerRepository();
            _credentialsService = new CredentialsService();
        }


        public async Task<IEnumerable<Entities.Server>> GetServersLocally()
        {
            var serverList = await _serverRepository.GetServers();

            serverList.LogToConsole();
            return serverList;
        }

        public async Task<IEnumerable<Entities.Server>> UpdateLocalServerData(string token = null)
        {
            if (token == null)
            {
                var user = await _credentialsService.GetUser();
                token = await ExceptionLogging.CatchAndLogErrors(async () => await _credentialsService.GetAuthorizationToken(user));
            }

            var client = new HttpClient
            {
                BaseAddress = new Uri($"{ConfigurationManager.AppSettings["baseApiAddress"]}"),
            };

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
            var result = await ExceptionLogging.CatchAndLogErrors(async () => await client.GetAsync("servers"));
            var resultContent = await result.Content.ReadAsStringAsync();
            var serverList = JsonConvert.DeserializeObject<List<Entities.Server>>(resultContent);

            await ExceptionLogging.CatchAndLogErrors(async () => await _serverRepository.SaveServers(serverList)) ;

            serverList.LogToConsole();
            return serverList;
        }
    }
}