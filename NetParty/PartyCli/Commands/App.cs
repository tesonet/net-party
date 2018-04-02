using McMaster.Extensions.CommandLineUtils;
using System;

namespace PartyCli.Commands
{
    public class App
    {
        public static CommandLineApplication Configure(CommandLineApplication app, IServiceProvider services)
        {
            app.Name = "PartyCLI";
            app.HelpOption();

            app.Command("config", command => ConfigCommand.Configure(command, services));
            app.Command("server_list", command => ServerListCommand.Configure(command, services));

            return app;
        }
    }
}
