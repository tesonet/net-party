using Autofac;
using NetParty.Interfaces.Services;
using NetParty.Services;

namespace NetParty.App.DI
{
    public class ServicesModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ConfigurationService>().As<IConfigurationService>();
            builder.RegisterType<ServerListService>().As<IServerListService>();
            builder.RegisterType<OutputService>().As<IOutputService>();
            builder.RegisterType<FluentValidationService>().As<IValidationService>();
        }
    }
}
