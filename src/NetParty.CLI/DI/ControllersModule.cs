using Autofac;
using Autofac.Extras.DynamicProxy;
using NetParty.CLI.Controllers;
using NetParty.CLI.Options;
using NetParty.CLI.Utils;

namespace NetParty.CLI.DI
{
    public class ControllersModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ConfigController>().As<IController<ConfigOptions>>().EnableInterfaceInterceptors().InterceptedBy(typeof(LoggingInterceptor));
            builder.RegisterType<ServerListController>().As<IController<ServerListOptions>>().EnableInterfaceInterceptors().InterceptedBy(typeof(LoggingInterceptor));

            builder.RegisterGenericDecorator(typeof(ExceptionHandlingDecorator<>), typeof(IController<>));
        }
    }
}