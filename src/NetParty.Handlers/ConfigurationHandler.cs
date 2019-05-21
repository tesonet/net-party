using System.Threading.Tasks;
using GuardNet;
using NetParty.Contracts;
using NetParty.Handlers.Base;
using NetParty.Contracts.Requests;
using NetParty.Repositories.Core;
using NetParty.Services.Interfaces;

namespace NetParty.Handlers
{
    public class ConfigurationHandler : BaseHandler<ConfigurationRequest>
    {
        private readonly ICredentialsRepository _credentialsRepository;
        private readonly IDisplayService _displayService;

        public ConfigurationHandler(
            ICredentialsRepository credentialsRepository, 
            IDisplayService displayService)
        {
            Guard.NotNull(credentialsRepository, nameof(credentialsRepository));
            Guard.NotNull(displayService, nameof(displayService));

            _credentialsRepository = credentialsRepository;
            _displayService = displayService;
        }

        public override async Task HandleBaseAsync(ConfigurationRequest request)
        {
            var credentials = new Credentials
            {
                UserName = request.UserName,
                Password = request.Password
            };

            await _credentialsRepository.SaveCredentialsAsync(credentials).ConfigureAwait(false);

            _displayService.DisplayText("Welcome to NetParty. Let's go to see servers list 'NetParty.Application.exe server_list'!");
        }
    }
}
