using System.Linq;
using System.Threading.Tasks;
using NetParty.Contracts.Requests;
using NetParty.Contracts.Results;
using NetParty.Domain.Exceptions;
using NetParty.Domain.Interfaces;

namespace NetParty.Handlers
{
    public class WhenGetLocalServerList : IRequestHandler<GetLocalServerList, ServerList>
    {
        private readonly ILocalServerRepository _localServerRepository;

        public WhenGetLocalServerList(ILocalServerRepository localServerRepository)
        {
            _localServerRepository = localServerRepository;
        }

        public async Task<ServerList> ThenAsync(GetLocalServerList request)
        {
            var servers = await _localServerRepository.Get().ConfigureAwait(false);

            if (servers == null)
            {
                throw DomainExceptions.CouldNotRetrieveLocalServers;
            }

            var serversList = servers.ToList();
            var result = new ServerList { Items = serversList, Count = serversList.Count };

            return result;
        }
    }
}