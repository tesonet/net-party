#region Using

using Autofac;
using NetParty.Application.CredentialsNS;

#endregion

namespace NetParty.Application.DependencyInjection.Modules
    {
    public class CredentialsModule : Module
        {
        protected override void Load(ContainerBuilder builder)
            {
            builder.RegisterType<CredentialsRepository>().As<ICredentialsRepository>();
            builder.Register(c => new FileStorageProvider("credentials.dat")).As<IStorageProvider>();
            }
        }
    }
