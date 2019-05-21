using System;
using System.Linq;
using Autofac;
using CommandLine;
using NetParty.Application.DI;
using NetParty.Application.Options;
using NetParty.Contracts.Requests;
using NetParty.Handlers.Base;
using Serilog;

namespace NetParty.Application
{
    class Program
    {
        public static void Main(string[] args)
        {
            var result = ParseActions(args);

            string argument;

            while (result)
            {
                argument = Console.ReadLine();

                if (argument == "exit")
                    return;

                if (String.IsNullOrEmpty(argument))
                {
                    Console.WriteLine("Please enter valid action name");
                    break;
                }

                result = ParseActions(argument.Split(' ').ToArray());
            }

            Console.WriteLine("'exit'?");
            argument = Console.ReadLine();
            if (argument == "exit")
                return;
        }

        private static bool ParseActions(string[] args)
        {
            return Parser.Default.ParseArguments<ConfigOption, ServerListOption>(args)
                .MapResult(
                    (ConfigOption opt) =>
                    {
                        var handler = ServicesContainer.Container.Resolve<IHandler<ConfigurationRequest>>();
                        var request = new ConfigurationRequest
                        {
                            UserName = opt.UserName,
                            Password = opt.Password
                        };

                        try
                        {
                            handler.HandleAsync(request).Wait();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            return false;
                        }

                        return true;
                    },
                    (ServerListOption opt) =>
                    {
                        var handler = ServicesContainer.Container.Resolve<IHandler<ServerListRequest>>();
                        var request = new ServerListRequest
                        {
                            Local = opt.Local
                        };

                        try
                        {
                            handler.HandleAsync(request).Wait();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            return false;
                        }

                        return true;
                    },
                    errors =>
                    {
                        var logger = ServicesContainer.Container.Resolve<ILogger>();

                        errors.ToList().ForEach(err => logger.Error("Parsing error {error}", err));

                        return true;
                    });
        }
    }
}
