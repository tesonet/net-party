﻿using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetPartyCore.Output;
using NetPartyCore.Controller;
using NetPartyCore.Framework;

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
                    .AddSingleton<IOutputFormatter, OutputFormatter>()
                    .BuildServiceProvider();

                var logger = serviceProvider
                    .GetService<ILoggerFactory>()
                    .CreateLogger<Program>();

                var router = new CommandRouter();

                router.AddRoute("config", "config", new List<Option>() {
                    new Option("--username", "Api client username", new Argument<string>()),
                    new Option("--password", "Api client password", new Argument<string>())
                }, CommandHandler.Create<string, string>((username, password) => {
                    CoreController
                        .CreateWithProvider<ConfigController>(serviceProvider)
                        .ConfigAction(username, password);
                }));

                router.AddRoute("server-list", "Server list management command", new List<Option>() {
                    new Option("--local", "Display servers from local storage", new Argument<bool>())
                }, CommandHandler.Create<bool>((local) => {
                    CoreController
                        .CreateWithProvider<ServerController>(serviceProvider)
                        .ServerListAction(local);
                }));

                // Parse the incoming args and invoke the handler
                return await router.GetRootCommand().InvokeAsync(args);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.ReadKey();
                return 0;
            }
        }
    }
}