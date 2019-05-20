using System;
using System.Diagnostics;
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
            Parser.Default.ParseArguments<ConfigOption, ServerListOption>(args)
                .MapResult(
                    (ConfigOption opt) => 
                    {
                        var handler = ServicesContainer.Container.Resolve<IHandler<ConfigurationRequest>>();
                        var request = new ConfigurationRequest
                        {
                            UserName = opt.UserName,
                            Password = opt.Password
                        };

                        handler.HandleAsync(request).Wait();

                        return true;
                    },
                    (ServerListOption opt) =>
                    {
                        var handler = ServicesContainer.Container.Resolve<IHandler<ServerListRequest>>();
                        var request = new ServerListRequest
                        {
                            Local = opt.Local
                        };

                        handler.HandleAsync(request).Wait();

                        return true;
                    },
                    errors =>
                    {
                        var logger = ServicesContainer.Container.Resolve<ILogger>();

                        errors.ToList().ForEach(err => logger.Error("Parsing error {error}", err));

                        return true;
                    });

            if (Debugger.IsAttached)
                Console.ReadLine();
        }
    }
}
