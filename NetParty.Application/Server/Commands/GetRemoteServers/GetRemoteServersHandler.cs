using MediatR;
using NetParty.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using NetParty.Application.Extensions;
using NetParty.Application.Server.Commands.Models;

namespace NetParty.Application.Server.Commands
{
    public class GetRemoteServersHandler : IRequestHandler<GetRemoteServersCommand, ICommandResult>
    {
        private IApi _api;
        private IPersistance _persistance;
        private RemoteServersDto _result;
        public GetRemoteServersHandler(IApi api, IPersistance persistance)
        {
            _api = api;
            _persistance = persistance;
            _result = new RemoteServersDto();
        }

        public async Task<ICommandResult> Handle(GetRemoteServersCommand request, CancellationToken cancellationToken)
        {
            var credentials = await _persistance.GetCredentials();
            var token = await _api.GetAuthorizationToken(credentials.Username, credentials.Password);
            var remoteServers = await _api.GetServers(token);
            await _persistance.SaveServers(remoteServers.MapToEntities());
            _result.Servers = (await _persistance.GetServers()).MapToDto();
 
            return _result;
        }
    }
}
