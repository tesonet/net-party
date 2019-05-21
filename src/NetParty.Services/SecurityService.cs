using System.Threading.Tasks;
using GuardNet;
using NetParty.Clients.Interfaces;
using NetParty.Contracts;
using NetParty.Services.Interfaces;
using NetParty.Utils.Exceptions;

namespace NetParty.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly ICredentialsService _credentialsService;
        private readonly ITesonetClient _tesonetClient;

        public SecurityService(
            ICredentialsService credentialsService,
            ITesonetClient tesonetClient)
        {
            Guard.NotNull(credentialsService, nameof(credentialsService));
            Guard.NotNull(tesonetClient, nameof(tesonetClient));

            _credentialsService = credentialsService;
            _tesonetClient = tesonetClient;
        }

        public async Task<string> GetTokenAsync()
        {
            Credentials credentials = await _credentialsService.GetCredentialsAsync();
            if (credentials == null)
                throw new CredentialsException();

            var token = await _tesonetClient.GetTokenAsync(
                credentials.UserName, 
                credentials.Password);

            return token;
        }
    }
}
