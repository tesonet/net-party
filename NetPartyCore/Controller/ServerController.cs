using NetPartyCore.Datastore;
using NetPartyCore.Framework;
using NetPartyCore.Network;
using NetPartyCore.Output;
using System.Collections.Generic;
using System.CommandLine;

namespace NetPartyCore.Controller
{
    /**
     * Controller of "server-list" action invoked from command line
     */
    internal class ServerController : CoreController
    {
        public async void ServerListAction(bool local)
        {
            var datatore = GetSerivce<IStorage>();
            var output = GetSerivce<IOutputFormatter>();

            if (!local)
            {
                var client = datatore.GetConfiguration();
                var remoteApi = GetSerivce<IRemoteApi>();
                var token = await remoteApi.GetToken(client);
                var servers = await remoteApi.GetServers(token);
                datatore.SetSevers(servers);
            }

            output.PrintServers(datatore.GetServers());            
        }
    }
}
