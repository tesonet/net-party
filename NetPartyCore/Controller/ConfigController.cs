using NetPartyCore.Datastore;
using NetPartyCore.Datastore.Model;
using NetPartyCore.Exception;
using NetPartyCore.Framework;
using NetPartyCore.Output;
using System.Threading.Tasks;

namespace NetPartyCore.Controller
{
    /**
     * Controller of "config" action invoked from command line
     */
    internal class ConfigController : CoreController
    {
        public async Task ConfigAction(string username, string password)
        {
            if (username == null || username == "" || password == null || password == "")
            {
                throw new ConfigurationInvalidException();
            }

            var client = new Client()
            {
                Username = username,
                Password = password
            };

            GetSerivce<IStorage>().SetConfiguration(client);

            GetSerivce<IOutputFormatter>().PrintConfiguration(client);
        }
    }
}
