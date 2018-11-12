using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Logging;
using PartyCli.Core.Enums;
using PartyCli.Core.Interfaces;
using PartyCli.Infrastructure;
using PartyCli.Infrastructure.ComamndHandlers;
using PartyCli.Infrastructure.Presenters;
using PartyCli.Infrastructure.Repositories;
using PartyCli.Infrastructure.ServersApis;
//using Serilog;

namespace PartyCli
{
    public class Bootstraper
    {
        private ServiceCollection services;
        public static ServiceProvider Provider { get; private set; }


        public static void Configure()
        {
            var services = new ServiceCollection();

            IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("PartyCli.AppSettings.json", optional: true, reloadOnChange: true)
            .Build();

            services.AddSingleton<IConfiguration>(configuration);

            ConfigureServices(services);


            Provider = services.BuildServiceProvider();            

        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {


            serviceCollection
                .AddSingleton<ILogger, Logger>()
                .AddTransient<IPresenter, SimpleCliPresenter>()
                .AddTransient<IReceiver, Receiver>();

            serviceCollection
                .AddTransient<ICommandHandler, ConfigCommandHandler>()
                .AddTransient<ICommandHandler, ServerListCommandHandler>()
                .AddTransient<ICommandHandler, UnsuportedCommandHandler>()
                .AddTransient<Func<AveilableCommands, ICommandHandler>>(serviceProvider =>
                            key => serviceProvider.GetServices<ICommandHandler>()
                                    .First(s => s.Command == key)
                    );

            serviceCollection
                .AddTransient<IApiAuthCredentialsRepository, ApiAuthCredencialsFileRepository>()
                .AddTransient<IServersRepository, ServersFileRepository>();

            serviceCollection
                .AddTransient<IServersApi, WebServersApi>();

        }

    }
}
