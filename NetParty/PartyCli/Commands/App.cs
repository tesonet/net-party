using McMaster.Extensions.CommandLineUtils;
using Serilog;

namespace PartyCli.Commands
{
    public class App
    {
        ConfigCommand configCommand;
        ServerListCommand serverListCommand;

        public App(ILogger logger, ConfigCommand configCommand, ServerListCommand serverListCommand)
        {
            this.configCommand = configCommand;
            this.serverListCommand = serverListCommand;
        }

        public CommandLineApplication Configure(CommandLineApplication app)
        {
            app.Name = "PartyCLI";
            app.HelpOption();

            app.Command("config", command => configCommand.Configure(command));
            app.Command("server_list", command => serverListCommand.Configure(command));

            return app;
        }
    }
}
