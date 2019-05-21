using Autofac;
using NetParty.Services;
using NetParty.Services.Interfaces;

namespace NetParty.Application.DI
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CredentialsService>()
                .As<ICredentialsService>()
                .OwnedByLifetimeScope();

            builder.RegisterType<SecurityService>()
                .As<ISecurityService>()
                .OwnedByLifetimeScope();
            
            builder.RegisterType<ConsoleDisplayService>()
                .As<IDisplayService>()
                .OwnedByLifetimeScope();
        }
    }
}
