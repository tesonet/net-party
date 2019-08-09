using Unity;
using log4net;
using Unity.Injection;
using partycli.Api;
using partycli.Servers;
using partycli.Repository;
using partycli.Config;
using partycli.Http;
using partycli.Helpers;

namespace partycli
{
    public static class DependencyContainer
    {        
        public static readonly IUnityContainer container = RegisterElements();

        public static IUnityContainer RegisterElements()
        {
            var unityContainer = new UnityContainer();
            unityContainer.RegisterInstance<ILog>(LogManager.GetLogger("party-logger"));
            unityContainer.RegisterInstance<IPrinter>(new Printer(LogManager.GetLogger("party-printer")));

            unityContainer.RegisterInstance<IRepositoryProvider>("config",new FileRepositoryProvider(@"C:\Users\abrak\OneDrive\Desktop\config.txt")); //@"config.txt"));//(
            unityContainer.RegisterInstance<IHttpService>("config", new HttpService("http://playground.tesonet.lt/v1/tokens", unityContainer.Resolve<ILog>()));
            unityContainer.RegisterInstance<IAuthenticationRepository>(new AuthenticationRepository(httpService: unityContainer.Resolve<IHttpService>("config"), repositoryProvider: unityContainer.Resolve<IRepositoryProvider>("config")));

            unityContainer.RegisterInstance<IRepositoryProvider>("server_list", new FileRepositoryProvider(@"C:\Users\abrak\OneDrive\Desktop\server_list.txt"));//@"server_list.txt"));//(
            unityContainer.RegisterInstance<IHttpService>("server_list", new HttpService("http://playground.tesonet.lt/v1/servers", unityContainer.Resolve<ILog>()));
            unityContainer.RegisterInstance<IServersRepository>(new ServersRepository(httpService: unityContainer.Resolve<IHttpService>("server_list"), serversRepositoryProvider: unityContainer.Resolve<IRepositoryProvider>("server_list")));

            unityContainer.RegisterType<ApiHandler>(new InjectionConstructor(unityContainer.Resolve<IAuthenticationRepository>(), unityContainer.Resolve<IServersRepository>(), unityContainer.Resolve<IPrinter>()));

            return unityContainer;
        }
    }
}
