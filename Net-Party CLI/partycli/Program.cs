using partycli.Options;
using partycli.Api;
using log4net;
using Unity;
using CommandLine;

namespace partycli
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var container = DependencyContainer.container)
            {
                container.Resolve<ILog>().Debug("Net-Party CLI");
                var serversHandler = container.Resolve<ApiHandler>();

                Parser.Default.ParseArguments<ConfigSubOptions, ServerListSubOptions>(args)
                .WithParsed<ConfigSubOptions>(opts => serversHandler.SaveCredentials(opts.Username, opts.Password))
                .WithParsed<ServerListSubOptions>(opts =>
                    {
                        if (opts.Local)
                            serversHandler.GetServersListLocalAsync().Wait();
                        else
                            serversHandler.GetServersListAsync().Wait();
                    });
            }
        }
    }
}
