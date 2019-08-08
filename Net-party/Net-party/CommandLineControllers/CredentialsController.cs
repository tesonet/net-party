using System;
using Net_party.CommandLineModels;
using Net_party.Entities;
using Net_party.Services;

namespace Net_party.CommandLineControllers
{
    class CredentialsController
    {

        public CredentialsController(CredentialsDto data)
        {
            var service = new ConfigService();
            service.SaveUserInStorage(data);
        }
    }
}
