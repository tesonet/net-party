using Commander.NET;
using partycli.Options;
using partycli.Api;
using log4net;
using Unity;

namespace partycli
{
    class Program
    {
        static void Main(string[] args)
        {
            CommanderParser<CLIOptions> parser = new CommanderParser<CLIOptions>();
            CLIOptions options = parser.Add(args).Parse();
            using (var container = DependencyContainer.container)
            {
                var log = container.Resolve<ILog>();
                log.Info("Net-Party CLI");

                var serversHandler = container.Resolve<ApiHandler>();

                if (options.config != null) serversHandler.SaveCredentials(options.config.username, options.config.password);
                else if (options.server_list != null) serversHandler.GetServersListAsync().Wait();
                //serversHandler.GetServersListLocalAsync().Wait();
            }
            //TODO: Commander.NET has command, properties but is missing Flags. Use better CLI parser.
        }
    }
}
