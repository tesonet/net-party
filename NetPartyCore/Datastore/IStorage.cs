using NetPartyCore.Datastore.Model;
using System.Collections.Generic;
using System.Data.Linq;

namespace NetPartyCore.Datastore
{
    interface IStorage
    {

        void SetConfiguration(Client client);

        Client GetConfiguration();

        void SetSevers(List<Server> servers);

        List<Server> GetServers();

    }
}
