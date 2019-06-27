using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetPartyCore.Output;
using NetPartyCore.Controller;
using NetPartyCore.Framework;
using NetPartyCore.Datastore;
using NetPartyCore.Network;
using Refit;

namespace NetPartyCore
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            try
            {
                var serviceProvider = new ServiceCollection()
                    .AddLogging(loggingBuilder => loggingBuilder.AddConsole())
                    .AddSingleton<IRemoteApi>((_) => RestService.For<IRemoteApi>("http://playground.tesonet.lt/v1/"))
                    .AddSingleton<IOutputFormatter, OutputFormatter>()
                    .AddSingleton<IStorage, SQLiteStorage>()
                    .BuildServiceProvider();

                var logger = serviceProvider
                    .GetService<ILoggerFactory>()
                    .CreateLogger<Program>();

                await CreateCommandRouter(serviceProvider)
                    .InvokeAsync(args);
            }
            catch (System.Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            return 0;
        }

        private static RootCommand CreateCommandRouter(IServiceProvider serviceProvider)
        {
            var router = new CommandRouter();

            router.AddRoute("config", "config", new List<Option>() {
                new Option("--username", "Api client username", new Argument<string>()),
                new Option("--password", "Api client password", new Argument<string>())
            }, CommandHandler.Create<string, string>(async (username, password) => {
                // ideally I would not user try catch here but I couldn't find how to 
                // exploid  CommandLineBuilder UseExceptionHandler method and had no
                // more time to spare for this test
                try
                {
                    await CoreController
                        .CreateWithProvider<ConfigController>(serviceProvider)
                        .ConfigAction(username, password);
                }
                catch (System.Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }));

            router.AddRoute("server-list", "Server list management command", new List<Option>() {
                new Option("--local", "Display servers from local storage", new Argument<bool>())
            }, CommandHandler.Create<bool>(async (local) => {
                // ideally I would not user try catch here but I couldn't find how to 
                // exploid  CommandLineBuilder UseExceptionHandler method and had no
                // more time to spare for this test
                try
                {
                    await CoreController
                        .CreateWithProvider<ServerController>(serviceProvider)
                        .ServerListAction(local);
                }
                catch (System.Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }));

            return router.GetRootCommand();
        }
    }
}
