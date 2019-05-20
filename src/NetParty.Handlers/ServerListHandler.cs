using GuardNet;
using NetParty.Clients.Interfaces;
using NetParty.Contracts;
using NetParty.Contracts.Requests;
using NetParty.Handlers.Base;
using NetParty.Repositories.Core;
using NetParty.Services.Interfaces;
using System.Threading.Tasks;

namespace NetParty.Application.Handlers
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

        public async override Task HandleBaseAsync(ServerListRequest request)
        {
            ServerDto[] servers;

            if (request.Local)
            {
                servers = await _serversRepository.GetServersAsync();
            }
            else
            {
                string token = await _securityService.GetTokenAsync();

                ServerDto[] data = await _tesonetClient.GetServersAsync(token);

                servers = await _serversRepository.SaveServersAsync(data);
            }

            _displayService.DiplsayTable(servers);
        }
    }
}
