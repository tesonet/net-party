namespace Tesonet.ServerListApp.Infrastructure.ServerListApi
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using Application;
    using Autofac;
    using Http;
    using Microsoft.Extensions.Logging;
    using Storage;

    public class ServersListApiClientRegistrationModule : Module
    {
        private readonly string _baseAddress;

        public ServersListApiClientRegistrationModule(string baseAddress)
        {
            _baseAddress = baseAddress;
        }

        protected override void Load(ContainerBuilder builder)
        {
            RegisterAuthClient(builder, _baseAddress);
            RegisterServersListClient(builder, _baseAddress);
        }

        private static void RegisterServersListClient(ContainerBuilder builder, string baseUri)
        {
            const string RegistrationName = nameof(IServersListClient);

            builder
                .Register(ctx =>
                {
                    var authClient = ctx.Resolve<IAuthClient>();
                    var logger = ctx.Resolve<ILogger<IServersListClient>>();

                    var handler = new ServerListApiClientHandler(authClient, logger)
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
                        BaseAddress = new Uri(baseUri)
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

        private static void RegisterAuthClient(ContainerBuilder builder, string baseUri)
        {
            const string RegistrationName = nameof(IAuthClient);

            builder
                .Register(ctx =>
                {
                    var logger = ctx.Resolve<ILogger<IAuthClient>>();

                    return new LoggingHttpClientHandler(logger)
                    {
                        UseProxy = false,
                        UseDefaultCredentials = false
                    };
                })
                .Named<HttpClientHandler>(RegistrationName);

            builder
                .Register(ctx =>
                {
                    var handler = ctx.ResolveNamed<HttpClientHandler>(RegistrationName);

                    return new HttpClient(handler, false)
                    {
                        BaseAddress = new Uri(baseUri)
                    };
                })
                .Named<HttpClient>(RegistrationName);

            builder
                .Register(ctx =>
                {
                    var dbContext = ctx.Resolve<ServersDbContext>();
                    var client = ctx.ResolveNamed<HttpClient>(RegistrationName);

                    var credentials = dbContext.ClientCredentials.SingleOrDefault()
                        ?? ClientCredentials.Empty;

                    return new AuthClient(credentials, client);
                })
                .As<IAuthClient>();
        }
    }
}