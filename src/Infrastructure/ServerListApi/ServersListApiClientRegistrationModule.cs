namespace Tesonet.ServerListApp.Infrastructure.ServerListApi
{
    using System;
    using System.Net.Http;
    using Application;
    using Autofac;
    using Configuration;
    using Microsoft.Extensions.Logging;

    internal class ServersListApiClientRegistrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            const string RegistrationName = nameof(IServersListClient);
            var config = new PersistentJsonConfiguration().GetSection<ServersListApiConfig>("ServerListApi");

            builder
                .Register(ctx =>
                {
                    var logger = ctx.Resolve<ILogger<IServersListClient>>();
                    var credentials = config.ClientCredentials;

                    var handler = new ServersListApiClientHandler(credentials, logger)
                    {
                        UseProxy = false,
                        UseDefaultCredentials = false
                    };

                    return handler;
                })
                .SingleInstance()
                .Named<HttpClientHandler>(RegistrationName);

            builder
                .Register(ctx =>
                {
                    var handler = ctx.ResolveNamed<HttpClientHandler>(RegistrationName);

                    return new HttpClient(handler, false)
                    {
                        BaseAddress = new Uri(config.BaseAddress)
                    };
                })
                .Named<HttpClient>(RegistrationName);

            builder
                .RegisterType<ServersListClient>()
                .WithParameter(
                    (pi, _) => pi.ParameterType == typeof(HttpClient),
                    (_, ctx) => ctx.ResolveNamed<HttpClient>(RegistrationName))
                .As<IServersListClient>();
        }
    }
}