using NetPartyCore.Datastore.Model;
using System.Collections.Generic;

namespace NetPartyCore.Output
{
    interface IOutputFormatter
    {

        void TestMethod(string output);

        void PrintServers(List<Server> servers);

    }
}
