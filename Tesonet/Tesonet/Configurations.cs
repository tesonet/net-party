using SimpleInjector;

namespace Tesonet
{
    public static class Configurations
    {
        public static Container container = new Container();

        public static void Start()
        {         
            container.Register<ITesonetService, TesonetService>(Lifestyle.Singleton);
            container.Register<IFileService, FileService>(Lifestyle.Singleton);
            container.Register<ILogService, LogService>(Lifestyle.Singleton);

            container.Verify();
        }
    }
}
