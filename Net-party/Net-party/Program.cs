using System;
using System.Reflection;
using System.Threading.Tasks;
using CommandLine;
using Net_party.CommandLineModels;
using Net_party.Controllers;
using Net_party.Logging;
using Net_party.Services.Credentials;
using Net_party.Services.Server;
using Ninject;

namespace Net_party
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await RouteToControllers(args);
        }

        private static async Task RouteToControllers(string[] args)
        {
            if (args.Length == 0)
            {
                NLogger.GetInstance().Error("The application is supposed to be launched with arguments.");
                return;
            }

            Task controllerTask = null;
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            switch (args[0].ToLower())
            {
                case "config":
                    Parser.Default.ParseArguments<Credentials>(args)
                        .WithParsed(config =>
                        {
                            controllerTask = ExceptionLogging.CatchAndLogErrors(async () => await new CredentialsController(kernel.Get<ICredentialsService>()).SaveUser(config), rethrow: false);
                        });
                    break;
                case "server_list":
                    Parser.Default.ParseArguments<ServersRetrievalConfiguration>(args)
                        .WithParsed(config =>
                        {
                            controllerTask = ExceptionLogging.CatchAndLogErrors(async () => await new ServerController(kernel.Get<IServerService>()).GetServers(config), rethrow: false);
                        });
                    break;
                default:
                    NLogger.GetInstance().Error("Command not recognised");
                    return;
            }

            if (controllerTask != null)
            {
                await controllerTask;
            }
        }
    }
}
