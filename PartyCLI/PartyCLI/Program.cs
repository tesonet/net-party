using System;
using System.Collections.Generic;

namespace PartyCLI
{
    using System.Configuration;
    using System.Data.Entity;

    using Autofac;

    using log4net;

    using ManyConsole;

    using PartyCLI.ApiConfigurations;
    using PartyCLI.ApiProviders;
    using PartyCLI.ConsoleOutputWriters;
    using PartyCLI.Data.Contexts;
    using PartyCLI.Data.Repositories;

    using RestSharp;

    public class Program
    {
        private static readonly ILog Logger = LogManager.GetLogger(AppDomain.CurrentDomain.FriendlyName);

        public static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += GlobalExceptionHandler;

            var builder = new ContainerBuilder();

            builder.RegisterType<ServerListOutputWriter>().As<IServerListOutputWriter>();
            builder.RegisterType<EfDbContext>().As<DbContext>();
            builder.RegisterType<EfGenericRepository>().As<IGenericRepository>();
            builder.Register(c => (TesonetPlaygroundApiConfiguration)ConfigurationManager.GetSection(nameof(TesonetPlaygroundApiConfiguration))).As<IApiConfiguration>();
            builder.RegisterType<RestClient>().As<IRestClient>();
            builder.RegisterType<JsonApiProvider>().As<ApiProvider>();
            builder.RegisterType<ConsoleOutputProvider>().As<IOutputProvider>();

            builder.RegisterAssemblyTypes(typeof(Program).Assembly)
                .Where(x => x.IsSubclassOf(typeof(ConsoleCommand)))
                .As<ConsoleCommand>();

            using var container = builder.Build();

            ConsoleCommandDispatcher.DispatchCommand(container.Resolve<IEnumerable<ConsoleCommand>>(), args, Console.Out);
        }

        static void GlobalExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            var errorId = Guid.NewGuid();

            Logger.Error(errorId, e.ExceptionObject as Exception);

            Console.WriteLine($@"An error with ID {errorId} has occured. Please check the application logs.");
            Console.WriteLine(Constants.ExitMessage);
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
