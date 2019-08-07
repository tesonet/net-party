using CommandLine;
using NetParty.CLI.Controllers;
using NetParty.CLI.Options;
using NetParty.CLI.Utils;

namespace NetParty.CLI
{
    public class Application
    {
        private readonly IController<ConfigOptions> _configController;
        private readonly IController<ServerListOptions> _serverListController;

        public Application(IController<ConfigOptions> configController, IController<ServerListOptions> serverListController)
        {
            _configController = configController;
            _serverListController = serverListController;
        }

        public void Run(string[] args)
        {
            Parser.Default.ParseArguments<ConfigOptions, ServerListOptions>(args)
                .WithParsed<ConfigOptions>(options => AsyncUtil.RunSync(() =>_configController.Handle(options)))
                .WithParsed<ServerListOptions>(options => AsyncUtil.RunSync(() => _serverListController.Handle(options)));
        }
    }
}
