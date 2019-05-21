using Autofac;
using NetParty.Repositories.Core;
using NetParty.Repositories.File;
using NetParty.Services;
using NetParty.Services.Interfaces;

namespace NetParty.Application.DI
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CredentialsRepository>()
                .As<ICredentialsRepository>()
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
