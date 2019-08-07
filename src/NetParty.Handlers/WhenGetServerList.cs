using System.Linq;
using System.Threading.Tasks;
using NetParty.Contracts.Requests;
using NetParty.Contracts.Results;
using NetParty.Domain.Interfaces;

namespace NetParty.Handlers
{
    public class WhenGetServerList : IRequestHandler<GetServerList, ServerList>
    {
        private readonly IServerRepository _serverRepository;
        private readonly ILocalServerRepository _localServerRepository;

        public WhenGetServerList(IServerRepository serverRepository, ILocalServerRepository localServerRepository)
        {
            _serverRepository = serverRepository;
            _localServerRepository = localServerRepository;
        }

        public async Task<ServerList> ThenAsync(GetServerList request)
        {
            var servers = await _serverRepository.Get().ConfigureAwait(false);

            var serversList = servers.ToList();
            await _localServerRepository.Save(serversList).ConfigureAwait(false);

            var result = new ServerList { Items = serversList, Count = serversList.Count };

            return result;
        }
    }
}