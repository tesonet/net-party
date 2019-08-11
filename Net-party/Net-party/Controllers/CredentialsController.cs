using Net_party.CommandLineModels;
using Net_party.Services;
using Net_party.Services.Config;
using Net_party.Services.Credentials;

namespace Net_party.CommandLineControllers
{
    class CredentialsController
    {
        private ICredentialsService _credentialsService;

        public CredentialsController()
        {
            _credentialsService = new CredentialsService();
        }

        public void SaveUser(CredentialsDto data)
        {
            _credentialsService.SaveUserInStorage(data);
        }
    }
}
