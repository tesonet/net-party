using Autofac;
using NetParty.Contracts.Requests;
using NetParty.Handlers;
using NetParty.Handlers.Base;

namespace NetParty.Application.DI
{
    public class HandlersModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ConfigurationHandler>()
                .As<IHandler<ConfigurationRequest>>()
                .PropertiesAutowired();

            builder.RegisterType<ServerListHandler>()
                .As<IHandler<ServerListRequest>>()
                .PropertiesAutowired();
        }
    }
}
