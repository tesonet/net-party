using NetPartyCore.Datastore;
using NetPartyCore.Datastore.Model;
using NetPartyCore.Exception;
using NetPartyCore.Framework;
using NetPartyCore.Network;
using NetPartyCore.Output;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetPartyCore.Controller
{
    /**
     * Controller of "server-list" action invoked from command line
     */
    internal class ServerController : CoreController
    {
        public async Task ServerListAction(bool local)
        {
            var output = GetSerivce<IOutputFormatter>();
            var datatore = GetSerivce<IStorage>();

            if (!local)
            {
                var token = await GetToken(GetConfiguration());
                var servers = await GetServers(token);
                
                datatore.SetSevers(servers);
            }

            output.PrintServers(datatore.GetServers());
        }

        private Client GetConfiguration()
        {
            var client = GetSerivce<IStorage>().GetConfiguration();
            
            if (client == null)
            {
                throw new ConfigurationNotFoundException();
            }

            return client;
        }

        private async Task<string> GetToken(Client client)
        {
            try
            {
                var tokenResponse = await GetSerivce<IRemoteApi>()
                    .GetToken(client.Username, client.Password);

                return tokenResponse.token;
            }
            catch
            {
                throw new TokenRetrievalException();
            }
        }

        private async Task<List<Server>> GetServers(string token)
        {
            try
            {
                var serversResponse = await GetSerivce<IRemoteApi>()
                    .GetServers($"Bearer {token}");

                return serversResponse.Select(x => new Server() {
                    Name = x.name,
                    Distance = x.distance
                })
                .ToList();
            }
            catch
            {
                throw new ServerListRetrievalException();
            }
        }
    }
}
