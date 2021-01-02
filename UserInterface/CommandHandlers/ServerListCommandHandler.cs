namespace Tesonet.ServerListApp.UserInterface.CommandHandlers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Application;
    using JetBrains.Annotations;
    using McMaster.Extensions.CommandLineUtils;

    [Command(Name = "server_list", Description = "Print a list of servers")]
    public class ServerListCommandHandler : BaseCommandHandler
    {
        private readonly ServerList _serverList;

        [Option("-l|--local", "Load data from persistent storage.", CommandOptionType.NoValue)]
        [UsedImplicitly]
        private bool LoadCachedServers { get; }

        public ServerListCommandHandler(ServerList serverList)
        {
            _serverList = serverList;
        }

        protected override async Task OnExecuteAsync(CommandLineApplication app)
        {
            IEnumerable<Server> servers;

            if (LoadCachedServers)
            {
                servers = await _serverList.GetCached();
            }
            else
            {
                servers = await _serverList.GetLatest();
            }

            servers.PrintToConsole();
        }
    }
}