namespace TesonetDotNetParty.IntegrationTests
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Autofac;
    using Autofac.Extensions.DependencyInjection;
    using FakeItEasy;
    using McMaster.Extensions.CommandLineUtils;
    using McMaster.Extensions.Hosting.CommandLine.Custom;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Tesonet.ServerListApp.Infrastructure;
    using Tesonet.ServerListApp.Infrastructure.Http;
    using Tesonet.ServerListApp.Infrastructure.ServerListApi;
    using Tesonet.ServerListApp.Infrastructure.Storage;
    using Tesonet.ServerListApp.UserInterface.CommandHandlers;

    public class CommandLineApplication : IAsyncDisposable
    {
        private readonly IConsole _console;
        private readonly IHost _host;
        private readonly CommandLineState _state;
        private readonly ServersDbContext _databaseContext;

        public string StandardOutput => _console.Out.ToString() ?? string.Empty;

        public string StandardError => _console.Error.ToString() ?? string.Empty;

        internal ServersListApiClientHandler FakeHttpClientHandler { get; }

        public CommandLineApplication(params string[] args)
        {
            _console = new StringOutputConsole();
            FakeHttpClientHandler = A.Fake<ServersListApiClientHandler>(o => o.CallsBaseMethods());

            _host = new HostBuilder()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>(HostConfiguration.Container)
                .ConfigureServices(HostConfiguration.Services)
                .ConfigureLogging(HostConfiguration.Logging)
                .ConfigureCommandLineApplication<RootCommandHandler>(args, out var state)
                .ConfigureContainer<ContainerBuilder>(containerBuilder =>
                {
                    containerBuilder.RegisterInstance(_console);
                    containerBuilder.RegisterInstance<HttpClientHandler>(FakeHttpClientHandler);
                    containerBuilder.RegisterInstance(new ServersListRenderer(false));
                })
                .Build();

            _state = state;
            _databaseContext = _host.Services.GetRequiredService<ServersDbContext>();
        }

        public async Task<int> RunAsync(Func<ServersDbContext, Task>? databaseContextFunc = null)
        {
            await _databaseContext.MigrateDatabase();
            _databaseContext.Servers.RemoveRange(_databaseContext.Servers);
            await _databaseContext.SaveChangesAsync();

            if (databaseContextFunc != null)
            {
                await databaseContextFunc(_databaseContext);
            }

            await _host.RunAsync();

            return _state.ExitCode;
        }

        public async ValueTask DisposeAsync()
        {
            const string BaseAddress = "https://playground.tesonet.lt/v1/";
            var defaultSettings = new
            {
                ServerListApi = new
                {
                    username = string.Empty,
                    password = string.Empty,
                    BaseAddress
                }
            };

            var filePath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
            var json = JsonSerializer.Serialize(defaultSettings, JsonOptions.Default);
            await File.WriteAllTextAsync(filePath, json);
        }
    }
}