using GuardNet;
using NetParty.Application.Exceptions;
using NetParty.Clients.Interfaces;
using NetParty.Contracts;
using NetParty.Services.Interfaces;
using System.Threading.Tasks;

namespace NetParty.Application.Services
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
