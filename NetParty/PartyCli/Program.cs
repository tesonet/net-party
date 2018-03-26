using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PartyCli.Api;
using PartyCli.Commands;
using PartyCli.Interfaces;
using PartyCli.Models;
using PartyCli.Presenters;
using PartyCli.Repositories;
using System;

namespace PartyCli
{
    class Program
    {
        static int Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var services = serviceCollection.BuildServiceProvider();

            var app = new CommandLineApplication();
            app.Name = "PartyCLI";
            app.HelpOption();

            app.Command("config", command => services.GetService<ConfigCommand>().Configure(command));
            app.Command("server_list", command => services.GetService<ServerListCommand>().Configure(command));

            try
            {
                return app.Execute(args);
            }
            catch (Exception ex)
            {
                app.Error.WriteLine(ex.Message);
                return -1;
            }
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            // Add commands
            serviceCollection.AddTransient<ConfigCommand, ConfigCommand>();
            serviceCollection.AddTransient<ServerListCommand, ServerListCommand>();

            // Add services
            serviceCollection.AddTransient<IRepository<Credentials>, FileRepository<Credentials>>();
            serviceCollection.AddTransient<IRepository<Server>, FileRepository<Server>>();
            serviceCollection.AddTransient<IServerApi, ServerApi>();
            serviceCollection.AddTransient<IServerPresenter, ServerPresenter>();

            // Add configuration
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile(path: "AppSettings.json", optional: false, reloadOnChange: false)
                .Build();

            // serviceCollection.Configure<Settings>(config.GetSection("SettingsSection"))
        }
    }
}
