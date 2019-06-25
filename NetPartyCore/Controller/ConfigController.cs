using NetPartyCore.Datastore;
using NetPartyCore.Datastore.Model;
using NetPartyCore.Framework;
using NetPartyCore.Output;

namespace NetPartyCore.Controller
{
    /**
     * Controller of "config" action invoked from command line
     */
    internal class ConfigController : CoreController
    {
        public async void ConfigAction(string username, string password)
        {
            var client = new Client()
            {
                Username = username,
                Password = password
            };

            GetSerivce<IStorage>()
                .SetConfiguration(client);

            GetSerivce<IOutputFormatter>()
                .PrintConfiguration(client);
        }
    }
}
