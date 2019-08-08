using Unity;
using log4net;
using Unity.Injection;

namespace partycli
{
    public static class DependencyContainer
    {        
        public static readonly IUnityContainer container = RegisterElements();

        public static IUnityContainer RegisterElements()
        {
            var unityContainer = new UnityContainer();
            unityContainer.RegisterInstance<ILog>(LogManager.GetLogger("party-logger"));

            unityContainer.RegisterInstance<IRepositoryProvider>("config",new FileRepositoryProvider(@"C:\Users\abrak\OneDrive\Desktop\config.txt"));
            unityContainer.RegisterType<AuthenticationRepository>(new InjectionConstructor(unityContainer.Resolve<IRepositoryProvider>("config")));
            unityContainer.RegisterInstance<IAuthenticationRepository>(new AuthenticationRepository(unityContainer.Resolve<IRepositoryProvider>("config")));

            return unityContainer;
        }
    }
}
