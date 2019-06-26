using NetPartyCore.Datastore;
using NetPartyCore.Datastore.Model;
using NetPartyCore.Framework;
using NetPartyCore.Network;
using NetPartyCore.Output;
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
                var client = datatore.GetConfiguration();
                var remoteApi = GetSerivce<IRemoteApi>();

                var tokenResponse = await remoteApi
                    .GetToken(client.Username, client.Password);

                var serversResponse = await remoteApi
                    .GetServers($"Bearer {tokenResponse.token}");

                var remoteServers = serversResponse
                    .Select(x => new Server() {
                        Name = x.name,
                        Distance = x.distance
                    })
                    .ToList();

                datatore.SetSevers(remoteServers);
            }

            output.PrintServers(datatore.GetServers());
        }
    }
}
