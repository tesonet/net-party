using Unity;
using log4net;
using partycli.Repository;
using partycli.Config;
using partycli.Http;

namespace partycli
{
    public static class DependencyContainer
    {        
        public static readonly IUnityContainer container = RegisterElements();

        public static IUnityContainer RegisterElements()
        {
            var unityContainer = new UnityContainer();
            unityContainer.RegisterInstance<ILog>(LogManager.GetLogger("party-logger"));

            unityContainer.RegisterInstance<IRepositoryProvider>("config",new FileRepositoryProvider(@"config.txt"));//(@"C:\Users\abrak\OneDrive\Desktop\config.txt"));
            unityContainer.RegisterInstance<IHttpService>("config", new HttpService("http://playground.tesonet.lt/v1/tokens"));
            unityContainer.RegisterInstance<IAuthenticationRepository>(new AuthenticationRepository(httpService: unityContainer.Resolve<IHttpService>("config"), repositoryProvider: unityContainer.Resolve<IRepositoryProvider>("config")));

            return unityContainer;
        }
    }
}
