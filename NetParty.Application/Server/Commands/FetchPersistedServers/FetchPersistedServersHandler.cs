using MediatR;
using NetParty.Application.Extensions;
using NetParty.Application.Interfaces;
using NetParty.Application.Server.Commands.Models;
using System.Threading;
using System.Threading.Tasks;

namespace NetParty.Application.Server.Commands
{
    public class FetchPersistedServers : IRequestHandler<FetchPersistedServersCommand, ICommandResult>
    {
        private IPersistance _persistance;
        private LocalServersDto _result;

        public FetchPersistedServers(IPersistance persistance)
        {
            _persistance = persistance;
            _result = new LocalServersDto();
        }

        public async Task<ICommandResult> Handle(FetchPersistedServersCommand request, CancellationToken cancellationToken)
        {
            var localServers = await _persistance.GetServers();
            _result.Servers = localServers.MapToDto();

            return _result;
        }
    }
}
