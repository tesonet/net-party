namespace Tesonet.ServerListApp.Infrastructure
{
    using Application;
    using Autofac;
    using Configuration;
    using Domain;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using ServerListApi;
    using Storage;

    public static class ConfigurationBuilder
    {
        public static void Container(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModule<ServersListApiClientRegistrationModule>();

            containerBuilder
                .RegisterType<PersistentJsonConfiguration>()
                .As<IPersistentConfiguration>()
                .SingleInstance();

            containerBuilder
                .RegisterType<ServersRepository>()
                .As<IServersRepository>();

            containerBuilder.RegisterType<ServerList>();
        }

        public static void Services(IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<ServersDbContext>();
        }

        public static void Logging(ILoggingBuilder loggingBuilder)
        {
            loggingBuilder
                .ClearProviders()
                .AddFile("activity.log")
                .SetMinimumLevel(LogLevel.Information);
        }
    }
}