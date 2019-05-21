using System.Threading.Tasks;
using GuardNet;
using NetParty.Clients.Interfaces;
using NetParty.Contracts;
using NetParty.Contracts.Requests;
using NetParty.Handlers.Base;
using NetParty.Repositories.Core;
using NetParty.Services.Interfaces;

namespace NetParty.Handlers
{
    public class ServerListHandler : BaseHandler<ServerListRequest>
    {
        private readonly ISecurityService _securityService;
        private readonly ITesonetClient _tesonetClient;
        private readonly IServersRepository _serversRepository;
        private readonly IDisplayService _displayService;

        public ServerListHandler(
            ISecurityService securityService,
            ITesonetClient tesonetClient,
            IServersRepository serversRepository,
            IDisplayService displayService)
        {
            Guard.NotNull(securityService, nameof(securityService));
            Guard.NotNull(tesonetClient, nameof(tesonetClient));
            Guard.NotNull(serversRepository, nameof(serversRepository));
            Guard.NotNull(displayService, nameof(displayService));

            _securityService = securityService;
            _tesonetClient = tesonetClient;
            _serversRepository = serversRepository;
            _displayService = displayService;
        }

        public override async Task HandleBaseAsync(ServerListRequest request)
        {
            ServerDto[] servers;

            if (request.Local)
            {
                Logger.Debug("Getting servers from local storage...");
                servers = await _serversRepository.GetServersAsync().ConfigureAwait(false);
            }
            else
            {
                Logger.Debug("Getting servers from tesonet server...");
                string token = await _securityService.GetTokenAsync().ConfigureAwait(false);

                ServerDto[] data = await _tesonetClient.GetServersAsync(token).ConfigureAwait(false);

                Logger.Debug("Storing servers list into local storage...");
                servers = await _serversRepository.SaveServersAsync(data).ConfigureAwait(false);
            }

            _displayService.DisplayTable(servers);
        }
    }
}
