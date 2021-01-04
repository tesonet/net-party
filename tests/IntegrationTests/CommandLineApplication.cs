namespace TesonetDotNetParty.IntegrationTests
{
    using System;
    using System.IO;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Autofac;
    using Autofac.Extensions.DependencyInjection;
    using McMaster.Extensions.CommandLineUtils;
    using McMaster.Extensions.Hosting.CommandLine.Custom;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Tesonet.ServerListApp.Infrastructure;
    using Tesonet.ServerListApp.Infrastructure.Http;
    using Tesonet.ServerListApp.Infrastructure.Storage;
    using Tesonet.ServerListApp.UserInterface.CommandHandlers;

    public class CommandLineApplication : IAsyncDisposable
    {
        private readonly IConsole _console;
        private readonly IHost _host;
        private readonly CommandLineState _state;

        public string StandardOutput => _console.Out.ToString() ?? string.Empty;

        public string StandardError => _console.Error.ToString() ?? string.Empty;

        public CommandLineApplication(params string[] args)
        {
            _console = new StringOutputConsole();

            _host = new HostBuilder()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>(HostConfiguration.Container)
                .ConfigureServices(HostConfiguration.Services)
                .ConfigureLogging(HostConfiguration.Logging)
                .ConfigureCommandLineApplication<RootCommandHandler>(args, out var state)
                .ConfigureServices(collection => collection.AddSingleton(_console))
                .Build();

            _state = state;
        }

        public async Task<int> RunAsync()
        {
            await _host.RunAsync();
            return _state.ExitCode;
        }

        public async Task MigrateDatabase()
        {
            var dbContext = _host.Services.GetRequiredService<ServersDbContext>();
            await dbContext.MigrateDatabase();
        }

        public async ValueTask DisposeAsync()
        {
            var defaultSettings = new
            {
                ServerListApi = new
                {
                    Username = string.Empty,
                    Password = string.Empty,
                    BaseAddress = "https://playground.tesonet.lt/v1/"
                }
            };

            var filePath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
            var json = JsonSerializer.Serialize(defaultSettings, JsonOptions.Default);

            await File.WriteAllTextAsync(filePath, json);
        }
    }
}