using NetPartyCore.Datastore;
using NetPartyCore.Datastore.Model;
using NetPartyCore.Framework;
using NetPartyCore.Output;

namespace NetPartyCore.Controller
{
    /**
     * Controller of "config" action invoked from command line
     */
    class ConfigController : CoreController
    {
        public async void ConfigAction(string username, string password)
        {
            GetSerivce<IStorage>().SetConfiguration(new Client(username, password));

            GetSerivce<IOutputFormatter>()
                .TestMethod($"Client configuration saved: {username} {password}");
        }
    }
}
