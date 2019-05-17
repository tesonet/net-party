using Autofac;
using NetParty.Application.Handlers;
using NetParty.Application.Handlers.Base;
using NetParty.Application.Options;

namespace NetParty.Application.DI
{
    public class HandlersModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ConfigHandler>()
                .As<IHandler<ConfigOption>>()
                .PropertiesAutowired();
        }
    }
}
