using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CommandLine;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using partycli.Clients;
using partycli.Contracts.Clients;
using partycli.Contracts.DTOs;
using partycli.Contracts.Exceptions;
using partycli.Contracts.Repositories;
using partycli.Contracts.Services;
using partycli.Contracts.Verbs;
using partycli.DataAccess;
using partycli.Services;

namespace partycli
{
    class Program
    {
        private static IServiceProvider _serviceProvider;

        static async Task Main(string[] args)
        {
            var config = BuildConfiguration();

            _serviceProvider = BuildServiceProvider(config);

            var logger = _serviceProvider.GetService<ILogger<Program>>();
            var ui = _serviceProvider.GetService<IAppUi>();

            try
            {
                await MigrateTheDatabaseAsync();
                await DoWork(args, ui);
            }
            catch (PartyException pe)
            {
                logger.LogError(pe.Message);
                ui.Show(pe.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while running partycli.");
                ui.Show($"Error while running partycli. For more information see logs.");
            }

            DisposeServiceProvider();
        }

        private static async Task MigrateTheDatabaseAsync()
        {
            var context = _serviceProvider.GetService<PartyContext>();

            await context.Database.MigrateAsync();
        }

        private static async Task DoWork(string[] args, IAppUi ui)
        {
            var serverLedger = _serviceProvider.GetService<IServerLedger>();
            var configurationLedger = _serviceProvider.GetService<IConfigurationLedger>();

            await Parser.Default.ParseArguments<ConfigurationVerb, ServerListVerb>(args)
                .MapResult(
                    (ConfigurationVerb v) =>
                        configurationLedger.AddOrUpdateAsync(new ConfigurationDTO(v.Username, v.Password)),
                    async (ServerListVerb v) =>
                    {
                        var servers = await serverLedger.GetAllAsync(v.Local);
                        ui.Show(servers);
                    },
                    er => Task.FromResult(0));
        }

        private static ServiceProvider BuildServiceProvider(IConfigurationRoot configurationRoot)
        {
            var tesonetClientSettings = configurationRoot.GetSection(nameof(TesonetSettings)).Get<TesonetSettings>();

            var serviceCollection = new ServiceCollection()
                .AddLogging(b =>
                {
                    b.ClearProviders();
                    b.SetMinimumLevel(LogLevel.Debug);
                    b.AddNLog();
                })
                .AddTransient<IServerLedger, ServerLedger>()
                .AddTransient<IConfigurationLedger, ConfigurationLedger>()
                .AddTransient<IServerRepository, ServerRepository>()
                .AddTransient<IConfigurationRepository, ConfigurationRepository>()
                .AddTransient<IAppUi, ConsoleUi>()
                .AddDbContext<PartyContext>(o =>
                {
                    o.UseSqlite("Data Source=partycli.db", x => x.MigrationsAssembly("partycli.DataAccess"));
                })
                .AddSingleton(tesonetClientSettings);

            serviceCollection.AddHttpClient<IServerListClient, TesonetClient>(c =>
                {
                    c.BaseAddress = new Uri(tesonetClientSettings.BaseUrl);
                    c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                });

            return serviceCollection.BuildServiceProvider();
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
        }

        private static void DisposeServiceProvider()
        {
            if (_serviceProvider == null)
                return;

            if (_serviceProvider is IDisposable disposable)
                disposable.Dispose();
        }
    }
}
