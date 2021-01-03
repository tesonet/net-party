namespace TesonetDotNetParty.IntegrationTests
{
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Autofac;
    using Autofac.Extensions.DependencyInjection;
    using McMaster.Extensions.CommandLineUtils;
    using McMaster.Extensions.Hosting.CommandLine.Custom;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Tesonet.ServerListApp.Infrastructure;
    using Tesonet.ServerListApp.Infrastructure.Storage;
    using Tesonet.ServerListApp.UserInterface.CommandHandlers;
    using Xunit;

    public class IntegrationTests
    {
        private readonly StringOutputConsole _console = new();

        [Fact]
        public async Task TestAsync()
        {
            using var host = CreateHost(out var state, "asd");
            await MigrateDatabase(host);
            await host.RunAsync();

            Debug.WriteLine(_console.StandardOutput);
            Assert.Equal(255, state.ExitCode);
        }

        private IHost CreateHost(out CommandLineState appState, params string[] args)
        {
            var host = new HostBuilder()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>(ConfigurationBuilder.Container)
                .ConfigureServices(ConfigurationBuilder.Services)
                .ConfigureLogging(ConfigurationBuilder.Logging)
                .ConfigureCommandLineApplication<RootCommandHandler>(args, out var state)
                .ConfigureServices(collection => collection.AddSingleton<IConsole>(_console))
                .Build();

            appState = state;
            return host;
        }

        private async Task MigrateDatabase(IHost host)
        {
            var dbContext = host.Services.GetRequiredService<ServersDbContext>();
            await dbContext.MigrateDatabase();
        }
    }
}
