using System.Threading.Tasks;
using Net_party.CommandLineModels;
using Net_party.Services.Credentials;

namespace Net_party.Controllers
{
    class CredentialsController
    {
        private readonly ICredentialsService _credentialsService;

        public CredentialsController(ICredentialsService credentialsService)
        {
            _credentialsService = credentialsService;
        }

        public Task SaveUser(Credentials data)
        {
            _credentialsService.SaveUserInStorage(data);
            return Task.CompletedTask;
        }
    }
}
