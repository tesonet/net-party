using Net_party.Database;
using Ninject;
using Ninject.Modules;

namespace Net_party
{
    public class NinjectContainer : NinjectModule
    {

        public override void Load()
        {
            Bind<IStorage>().To<SqLiteDatabase>();
        }
    }
}
