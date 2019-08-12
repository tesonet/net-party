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
            unityContainer.RegisterInstance<IPrinter>(new Printer(LogManager.GetLogger("party-logger")));

            unityContainer.RegisterInstance<IRepositoryProvider>("config",new FileRepositoryProvider(@"..\config.txt"));
            unityContainer.RegisterInstance<IHttpService>("config", new HttpService("http://playground.tesonet.lt/v1/tokens"));
            unityContainer.RegisterInstance<IAuthenticationRepository>(new AuthenticationRepository(httpService: unityContainer.Resolve<IHttpService>("config"), repositoryProvider: unityContainer.Resolve<IRepositoryProvider>("config")));

            unityContainer.RegisterInstance<IRepositoryProvider>("server_list", new FileRepositoryProvider(@"..\server_list.txt"));
            unityContainer.RegisterInstance<IHttpService>("server_list", new HttpService("http://playground.tesonet.lt/v1/servers"));
            unityContainer.RegisterInstance<IServersRepository>(new ServersRepository(httpService: unityContainer.Resolve<IHttpService>("server_list"), serversRepositoryProvider: unityContainer.Resolve<IRepositoryProvider>("server_list")));

            unityContainer.RegisterType<ApiHandler>(new InjectionConstructor(unityContainer.Resolve<IAuthenticationRepository>(), unityContainer.Resolve<IServersRepository>(), unityContainer.Resolve<IPrinter>()));

            return unityContainer;
        }
    }
}
