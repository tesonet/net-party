using NetPartyCore.Datastore.Model;
using System.Collections.Generic;

namespace NetPartyCore.Output
{
    interface IOutputFormatter
    {
        void PrintConfiguration(Client client);

        void PrintServers(List<Server> servers);
    }
}
