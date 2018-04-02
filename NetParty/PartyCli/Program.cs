using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PartyCli.Api;
using PartyCli.Commands;
using PartyCli.Interfaces;
using PartyCli.Models;
using PartyCli.Options;
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
            var app = App.Configure(new CommandLineApplication(), services);

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

        private static void ConfigureServices(IServiceCollection services)
        {
            // Build configuration
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile(path: "AppSettings.json", optional: false, reloadOnChange: false)
                .Build();

            // Add logging
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .CreateLogger();
     
            services.AddSingleton<ILogger>(Log.Logger);

            // Add commands
            services.AddTransient<App, App>();
            services.AddTransient<ConfigCommand, ConfigCommand>();
            services.AddTransient<ServerListCommand, ServerListCommand>();

            // Add services
            services.AddTransient<IRepository<Credentials>, FileRepository<Credentials>>();
            services.AddTransient<IRepository<Server>, FileRepository<Server>>();
            services.AddTransient<IServerApi, ServerApi>();
            services.AddTransient<IServerPresenter, ServerPresenter>();

            // Add configuration options
            services.AddOptions();
            services.Configure<ServerApiOptions>(config.GetSection("Api").GetSection("ServerApi"));
            FileRepository.Configure(config.GetSection("Repository").GetSection("FileRepository").Get<FileRepositoryOptions>());
        }
    }
}
