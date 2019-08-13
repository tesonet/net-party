using Net_party.Database;
using Net_party.Repositories.Credentials;
using Net_party.Repositories.Server;
using Net_party.Services.Credentials;
using Net_party.Services.Server;
using Ninject.Modules;

namespace Net_party
{
    public class NinjectContainer : NinjectModule
    {

        public override void Load()
        {
            Bind<IStorage>().To<SqLiteDatabase>();
            Bind<ICredentialsRepository>().To<CredentialsRepository>();
            Bind<IServerRepository>().To<ServerRepository>();
            Bind<ICredentialsService>().To<CredentialsService>();
            Bind<IServerService>().To<ServerService>();

        }
    }
}
