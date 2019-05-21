using System.Threading.Tasks;
using GuardNet;
using NetParty.Clients.Interfaces;
using NetParty.Contracts;
using NetParty.Repositories.Core;
using NetParty.Services.Interfaces;
using NetParty.Utils.Exceptions;

namespace NetParty.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly ICredentialsRepository _credentialsRepository;
        private readonly ITesonetClient _tesonetClient;

        public SecurityService(
            ICredentialsRepository credentialsRepository,
            ITesonetClient tesonetClient)
        {
            Guard.NotNull(credentialsRepository, nameof(credentialsRepository));
            Guard.NotNull(tesonetClient, nameof(tesonetClient));

            _credentialsRepository = credentialsRepository;
            _tesonetClient = tesonetClient;
        }

        public async Task<string> GetTokenAsync()
        {
            Credentials credentials = await _credentialsRepository.GetCredentialsAsync();
            if (credentials == null)
                throw new CredentialsException();

            var token = await _tesonetClient.GetTokenAsync(
                credentials.UserName, 
                credentials.Password);

            return token;
        }
    }
}
