namespace Tesonet.ServerListApp.Infrastructure
{
    using Application;
    using Autofac;
    using Domain;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using ServerListApi;
    using Storage;

    public static class HostBuilderExtensions
    {
        public static IHostBuilder ConfigureDefault(this IHostBuilder hostBuilder)
        {
            hostBuilder
                .ConfigureLogging(LoggingConfigurator)
                .ConfigureServices(ServicesConfigurator)
                .ConfigureContainer<ContainerBuilder>(ContainerConfigurator);

            return hostBuilder;
        }

        private static void ContainerConfigurator(ContainerBuilder containerBuilder)
        {
            const string BaseAddress = "https://playground.tesonet.lt/v1/";
            var serversListModule = new ServersListApiClientRegistrationModule(BaseAddress);
            containerBuilder.RegisterModule(serversListModule);

            containerBuilder.RegisterType<ServerList>();

            containerBuilder
                .RegisterType<ServersRepository>()
                .As<IServersRepository>();
        }

        private static void ServicesConfigurator(IServiceCollection collection)
        {
            collection.AddDbContext<ServersDbContext>();
        }

        private static void LoggingConfigurator(ILoggingBuilder builder)
        {
            builder
                .ClearProviders()
                .AddFile("activity.log")
                .SetMinimumLevel(LogLevel.Information);
        }
    }
}