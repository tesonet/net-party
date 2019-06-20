using NetPartyCore.Datastore;
using NetPartyCore.Datastore.Model;
using NetPartyCore.Framework;
using NetPartyCore.Output;
using System.Data.Linq;

namespace NetPartyCore.Controller
{
    /**
     * Controller of "config" action invoked from command line
     */
    class ConfigController : CoreController
    {
        
        // what config action should do?
        // it should open persistant storage and put/update those values there
        // that's it
        public void ConfigAction(string username, string password)
        {
            var context = this.GetSerivce<IStorage>();
            
            GetSerivce<IOutputFormatter>().TestMethod($"ConfigController ConfigAction {username} {password}");
        }
    }
}
