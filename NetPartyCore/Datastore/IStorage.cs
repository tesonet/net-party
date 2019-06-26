using NetPartyCore.Datastore.Model;
using System.Collections.Generic;

namespace NetPartyCore.Datastore
{
    internal interface IStorage
    {
        void SetConfiguration(Client client);

        void SetSevers(List<Server> servers);

        Client GetConfiguration();

        List<Server> GetServers();
    }
}
