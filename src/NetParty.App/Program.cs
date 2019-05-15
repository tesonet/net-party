using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using CommandLine;
using NetParty.App.ConsoleOptions;
using NetParty.Contracts;
using NetParty.App.DI;
using NetParty.Contracts.Exceptions;
using NetParty.Interfaces.Services;
using Serilog;

namespace NetParty.App
{
    class Program
    {
        private static IContainer Container { get; set; }

        static void Main(string[] args)
        {
            InitLogging();
            InitDependencyInjection();

            using (var scope = Container.BeginLifetimeScope())
            {
                try
                {
                    var configurationService = scope.Resolve<IConfigurationService>();
                    var serverListService = scope.Resolve<IServerListService>();

                    Parser.Default.ParseArguments<ConfigOptions, ServerListOptions>(args)
                        .MapResult<ConfigOptions, ServerListOptions, bool>(
                            configOptions => StoreCredentials(configurationService,
                                new Credentials(configOptions.Username, configOptions.Password)),
                            serverListOptions => LoadServerList(serverListService, serverListOptions),
                            HandleErrors);
                }
                catch (AggregateException ex)
                {
                    HandleExceptions(ex);
                }

                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
            }
        }

        private static void HandleExceptions(AggregateException ex)
        {
            foreach (var innerException in ex.InnerExceptions)
            {
                if (innerException is BaseValidationException validationException)
                {
                    Log.Warning("Validation error: {exception}", validationException.Message);
                }
                else
                {
                    Log.Fatal(ex, "Fatal error.");
                }
            }
        }

        private static bool HandleErrors(IEnumerable<Error> errors)
        {
            errors.ToList().ForEach(err => Log.Error("ArgumentParsing error {error}", err));
            return true;
        }

        private static bool LoadServerList(IServerListService serverListService, ServerListOptions x)
        {
            serverListService.PrintServerList(x.Local).Wait();
            return true;
        }

        private static bool StoreCredentials(IConfigurationService configurationService, Credentials credentials)
        {
            configurationService.StoreCredentials(credentials).Wait();
            return true;
        }

        private static void InitLogging()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("app.log", rollingInterval: RollingInterval.Month)
                .CreateLogger();
        }

        private static void InitDependencyInjection()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<ClientsModule>();
            builder.RegisterModule<ValidatorsModule>();
            builder.RegisterModule<RepositoriesModule>();
            builder.RegisterModule<ServicesModule>();
            Container = builder.Build();
        }
    }
}
