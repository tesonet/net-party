using System.Threading.Tasks;
using GuardNet;
using NetParty.Contracts;
using NetParty.Handlers.Base;
using NetParty.Contracts.Requests;
using NetParty.Services.Interfaces;

namespace NetParty.Handlers
{
    public class ConfigurationHandler : BaseHandler<ConfigurationRequest>
    {
        private readonly ICredentialsService _credentialsService;

        public ConfigurationHandler(ICredentialsService credentialsService)
        {
            Guard.NotNull(credentialsService, nameof(credentialsService));

            _credentialsService = credentialsService;
        }

        public override Task HandleBaseAsync(ConfigurationRequest request)
        {
            var credentials = new Credentials
            {
                UserName = request.UserName,
                Password = request.Password
            };

            return _credentialsService.SaveCredentialsAsync(credentials);
        }
    }
}
