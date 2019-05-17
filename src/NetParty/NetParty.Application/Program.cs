using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using Autofac;
using CommandLine;
using CommandLine.Text;
using NetParty.Application.DI;
using NetParty.Application.Handlers.Base;
using NetParty.Application.Options;
using Serilog;

namespace NetParty.Application
{
    class Program
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<ConfigOption>(args)
                .MapResult(
                    (ConfigOption opt) => ResolveOptionHandler(opt),
                    errors =>
                    {
                        var logger = ServicesContainer.Container.Resolve<ILogger>();

                        errors.ToList().ForEach(err => logger.Error("Parsing error {error}", err));

                        return true;
                    });

            if (Debugger.IsAttached)
                Console.ReadLine();
        }

        private static bool ResolveOptionHandler<T>(T opt)
            where T : class
        {
            var handler = ServicesContainer.Container.Resolve<IHandler<T>>();
            handler.HandleAsync(opt).Wait();

            return true;
        }
    }
}
