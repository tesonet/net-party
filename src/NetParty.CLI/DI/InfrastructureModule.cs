using System;
using System.Net.Http;
using System.Configuration;
using Autofac;
using Autofac.Extras.DynamicProxy;
using NetParty.CLI.Utils;
using NetParty.Domain.Interfaces;
using NetParty.Infrastructure.Repositories;

namespace NetParty.CLI.DI
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(ctx => new HttpClient
                    {BaseAddress = new Uri(ConfigurationManager.AppSettings["ServersApiBaseUrl"])})
                    .Named<HttpClient>("tesonet")
                    .SingleInstance();
            builder.Register(ctx => new ServerRepository(ctx.ResolveNamed<HttpClient>("tesonet"),
                    ctx.Resolve<ILocalConfigurationRepository>()))
                    .As<IServerRepository>()
                    .EnableInterfaceInterceptors()
                    .InterceptedBy(typeof(LoggingInterceptor));
            builder.RegisterType<LocalServerRepository>()
                    .As<ILocalServerRepository>().SingleInstance()
                    .SingleInstance()
                    .EnableInterfaceInterceptors()
                    .InterceptedBy(typeof(LoggingInterceptor));
            builder.RegisterType<LocalConfigurationRepository>()
                    .As<ILocalConfigurationRepository>()
                    .SingleInstance()
                    .EnableInterfaceInterceptors()
                    .InterceptedBy(typeof(LoggingInterceptor));
        }
    }
}