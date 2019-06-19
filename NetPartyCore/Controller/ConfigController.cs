using NetPartyCore.Framework;
using NetPartyCore.Output;

namespace NetPartyCore.Controller
{
    /**
     * Controller of "config" action invoked from command line
     */
    class ConfigController : CoreController
    {
        public void ConfigAction(string username, string password)
        {
            GetSerivce<IOutputFormatter>().TestMethod($"ConfigController ConfigAction {username} {password}");
        }
    }
}
