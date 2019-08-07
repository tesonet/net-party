using System.Threading.Tasks;
using NetParty.Contracts.Requests;
using NetParty.Contracts.Results;
using NetParty.Domain.Interfaces;

namespace NetParty.Handlers
{
    public class WhenAuthorize : IRequestHandler<Authorize, AuthorizationResult>
    {
        private readonly IServerRepository _serverRepository;
        private readonly ILocalConfigurationRepository _localConfigurationRepository;

        public WhenAuthorize(IServerRepository serverRepository, ILocalConfigurationRepository localConfigurationRepository)
        {
            _serverRepository = serverRepository;
            _localConfigurationRepository = localConfigurationRepository;
        }

        public async Task<AuthorizationResult> ThenAsync(Authorize request)
        {
            var authData = await _serverRepository.Authorize(request.Username, request.Password).ConfigureAwait(false);
            await _localConfigurationRepository.SaveAuthorizationData(authData).ConfigureAwait(false);

            var result = new AuthorizationResult {Message = "Authorization successful"};
            return result;
        }
    }
}