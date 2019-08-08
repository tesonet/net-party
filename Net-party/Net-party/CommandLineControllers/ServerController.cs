using Net_party.CommandLineModels;
using Net_party.Services;

namespace Net_party.CommandLineControllers
{
    class ServerController
    {
        public ServerController(ServersRetrievalConfigurationDto data)
        {
            var service = new ConfigService();
            service.SaveUserInStorage(data);
        }
    }
}
