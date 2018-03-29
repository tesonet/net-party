using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PartyCli.Api;
using PartyCli.Commands;
using PartyCli.Interfaces;
using PartyCli.Models;
using PartyCli.Presenters;
using PartyCli.Repositories;
using Serilog;
using System;

namespace PartyCli
{
    class Program
    {
        static int Main(string[] args)
        {
            // Add services
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var services = serviceCollection.BuildServiceProvider();

            var logger = services.GetService<ILogger>().ForContext<Program>();

            // Configure CLI
            var app = services.GetService<App>().Configure(new CommandLineApplication());

            try
            {
                logger.Information("The application is started with {ArgumentCount} arguments: {Arguments}", args.Length, string.Join(" ", args));

                return app.Execute(args);
            }
            catch (Exception ex)
            {
                app.Error.WriteLine(ex.Message);
                logger.Error("Error message: {ErrorMessage}. Details: {ErrorDetails}", ex.Message, ex.ToString());
                return -1;
            }
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            // Build configuration
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile(path: "AppSettings.json", optional: false, reloadOnChange: false)
                .Build();

            // Add logging
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .CreateLogger();
     
            serviceCollection.AddSingleton<ILogger>(Log.Logger);

            // Add commands
            serviceCollection.AddTransient<App, App>();
            serviceCollection.AddTransient<ConfigCommand, ConfigCommand>();
            serviceCollection.AddTransient<ServerListCommand, ServerListCommand>();

            // Add services
            serviceCollection.AddTransient<IRepository<Credentials>, FileRepository<Credentials>>();
            serviceCollection.AddTransient<IRepository<Server>, FileRepository<Server>>();
            serviceCollection.AddTransient<IServerApi, ServerApi>();
            serviceCollection.AddTransient<IServerPresenter, ServerPresenter>();

            // Add service configurations
            //serviceCollection.Configure<Settings>(config.GetSection("SettingsSection"))
        }
    }
}
