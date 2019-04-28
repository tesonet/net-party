using NetParty.Application.Interfaces;
using System;
using NetParty.Persistance;
using NetParty.Application.CommandExecutor;
using NetParty.Api;
using System.Collections.Generic;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NetParty.Application.Server.Commands;
using System.Reflection;

namespace partycli
{
    class Program
    {
        private static ServiceProvider serviceProvider;

        static void Main(string[] args)
        {
            try
            {
                // Test only
                //args = new string[] { "config", "--username", "tesonet", "--password", "partyanimal" };
                //args = new string[] { "server_list", "--local" };
                //args = new string[] { "server_list" };
                //

                RegisterComponents();
                var commandArguments = ParseToSingleCommandParameters(args);

                var commandsExecutor = serviceProvider.GetService<ICommandsExecutor>();
                var result = commandsExecutor.Execute(commandArguments).Result as List<ICommandResult>;

                PrintResults(result);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.Read();
            }
            Console.Read();
        }

        private static void PrintResults(List<ICommandResult> result)
        {
            if (result == null || result.Count == 0)
            {
                Console.WriteLine("None commands was executed.");
            }
            else
            {
                // At the moment it supports just one command execution at the time
                var firstCommandTextResult = result[0].GetText();
                Console.WriteLine(firstCommandTextResult);
            }
        }

        //TODO: clean it...
        private static IEnumerable<ICommandArgs> ParseToSingleCommandParameters(string[] args)
        {
            var parsedArguments = new List<ConsoleCommand>();

            if (args.Length > 0)
            {
                var command = new ConsoleCommand
                {
                    CommandName = args[0],
                    CommandParameters = new Dictionary<string, string>()
                };
                string parameterName = null;

                for (int i = 1; i < args.Length; i++)
                {
                    if (parameterName == null)
                    {
                        parameterName = args[i].Replace("-", string.Empty);
                    }
                    else
                    {
                        command.CommandParameters.Add(parameterName, args[i]);
                        parameterName = null;
                    }
                }

                if (parameterName != null)
                {
                    command.CommandParameters.Add(parameterName, null);
                }

                parsedArguments.Add(command);
            }

            return parsedArguments;
        }

        private static void RegisterComponents()
        {
            var services = new ServiceCollection();
            services.AddScoped<IApi, PlaygroundApi>();
            services.AddScoped<IPersistance, PersistanceInMemorryService>();
            services.AddScoped<ICommandFacade, CommandFacade>();
            services.AddScoped<ICommandsExecutor, CommandsExecutor>();
            services.AddMediatR(typeof(GetRemoteServersHandler).GetTypeInfo().Assembly);
            serviceProvider = services.BuildServiceProvider();
        }
    }
}
