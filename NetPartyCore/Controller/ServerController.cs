using NetPartyCore.Datastore;
using NetPartyCore.Datastore.Model;
using NetPartyCore.Framework;
using NetPartyCore.Network;
using NetPartyCore.Output;
using System.Linq;

namespace NetPartyCore.Controller
{
    /**
     * Controller of "server-list" action invoked from command line
     */
    internal class ServerController : CoreController
    {
        public async void ServerListAction(bool local)
        {
            var output = GetSerivce<IOutputFormatter>();
            var datatore = GetSerivce<IStorage>();

            if (!local)
            {
                var client = datatore.GetConfiguration();
                var remoteApi = GetSerivce<IRemoteApi>();

                var tokenResponse = await remoteApi
                    .GetToken(client.Username, client.Password);

                var serversResponse = await remoteApi
                    .GetServers($"Bearer {tokenResponse.token}");

                var servers = serversResponse
                    .Select(x => new Server() {
                        Name = x.name,
                        Distance = x.distance
                    })
                    .ToList();

                datatore.SetSevers(servers);
            }

            output.PrintServers(datatore.GetServers());
        }
    }
}
