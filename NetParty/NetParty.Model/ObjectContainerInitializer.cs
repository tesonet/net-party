using NetParty.Common.Log;
using NetParty.Model.Repositories;
using NetParty.Model.Services;
using SimpleInjector;

namespace NetParty.Model
{
    public static class ObjectContainerInitializer
    {
        public static void Init(Container container)
        {
            container.Register<IRepository, Repository>(Lifestyle.Singleton);
            container.Register<IService, Service>(Lifestyle.Singleton);
            container.Register<IApiService, ApiService>(Lifestyle.Singleton);
            container.Register<ILogProvider, LogProvider>(Lifestyle.Singleton);
        }
    }
}