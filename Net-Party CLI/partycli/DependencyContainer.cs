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
            return unityContainer;
        }


    }
}
