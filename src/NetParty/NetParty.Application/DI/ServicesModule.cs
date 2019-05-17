using Autofac;

namespace NetParty.Application.DI
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CredentialsService>()
                .As<ICredentialsService>()
                .OwnedByLifetimeScope();
        }
    }
}
