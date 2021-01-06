namespace Tesonet.ServerListApp.UserInterface.CommandHandlers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Application;
    using Infrastructure;
    using JetBrains.Annotations;
    using McMaster.Extensions.CommandLineUtils;
    using static HelpText;

    [Command(Name = "server_list", Description = Command.ServerList)]
    public class ServersListCommandHandler : BaseCommandHandler
    {
        private readonly ServersList _serversList;
        private readonly ServersListRenderer _renderer;

        [Option("-l|--local", Option.Local, CommandOptionType.NoValue)]
        [UsedImplicitly]
        private bool LoadCachedServers { get; }

        public ServersListCommandHandler(ServersList serversList, ServersListRenderer renderer)
        {
            _serversList = serversList;
            _renderer = renderer;
        }

        protected override async Task OnExecuteAsync(CommandLineApplication app)
        {
            IEnumerable<Server> servers;

            if (LoadCachedServers)
            {
                servers = await _serversList.GetCached();
            }
            else
            {
                servers = await _serversList.GetLatest();
            }

            _renderer.Render(servers);
        }
    }
}