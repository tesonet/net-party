using System.Threading.Tasks;
using NetParty.CLI.Options;
using NetParty.CLI.ResultsPrinter;
using NetParty.Contracts.Requests;
using NetParty.Contracts.Results;
using NetParty.Handlers;

namespace NetParty.CLI.Controllers
{
    public class ServerListController : ControllerBase, IController<ServerListOptions>
    {
        private readonly IRequestHandler<GetServerList, ServerList> _whenGetServerList;
        private readonly IRequestHandler<GetLocalServerList, ServerList> _whenGetLocalServerList;

        public ServerListController(IRequestHandler<GetServerList, ServerList> whenGetServerList,
            IRequestHandler<GetLocalServerList, ServerList> whenGetLocalServerList, IResultsPrinter resultsPrinter) : base(resultsPrinter)
        {
            _whenGetServerList = whenGetServerList;
            _whenGetLocalServerList = whenGetLocalServerList;
        }

        public async Task Handle(ServerListOptions options)
        {
            ServerList servers = new ServerList();

            if (options.Local == false)
            {
                var request = new GetServerList();
                servers = await _whenGetServerList.ThenAsync(request).ConfigureAwait(false);
            }

            if (options.Local)
            {
                var request = new GetLocalServerList();
                servers = await _whenGetLocalServerList.ThenAsync(request).ConfigureAwait(false);
            }

            ResultsPrinter.Print(servers);
        }
    }
}