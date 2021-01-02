namespace Tesonet.ServerListApp.UserInterface
{
    using System.Threading.Tasks;
    using Autofac.Extensions.DependencyInjection;
    using CommandHandlers;
    using Infrastructure;
    using McMaster.Extensions.Hosting.CommandLine.Custom;
    using Microsoft.Extensions.Hosting;
    using static Infrastructure.Storage.ServersDbContext;

    public static class Program
    {
        public static async Task<int> Main(string[] args)
        {
            await MigrateDatabase();

            using var host = new HostBuilder()
                .ConfigureDefault()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureCommandLineApplication<RootCommandHandler>(args, out var applicationState)
                .Build();

            await host.RunAsync();
            return applicationState.ExitCode;
        }
    }
}