using NetPartyCore.Framework;
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
        public void ServerListAction(bool local)
        {
            GetSerivce<IOutputFormatter>().TestMethod($"ServerController ServerListAction {local}");
        }
    }
}
