using Autofac;
using NetParty.Repositories.Core;
using NetParty.Repositories.File;

namespace NetParty.Application.DI
{
    public class RepositoriesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ServersRepository>()
                .As<IServersRepository>()
                .InstancePerDependency();
        }
    }
}
