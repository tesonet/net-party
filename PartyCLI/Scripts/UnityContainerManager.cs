using Unity;

namespace PartyCLI
{
    public class UnityContainerManager
    {
        private UnityContainer container;

        public UnityContainerManager()
        {
            container = new UnityContainer();

            container.RegisterType<IDataFilesManager, DataFilesManager>();
            container.RegisterType<IServerRequestsManager, ServerRequestsManager>();
            container.RegisterType<IDisplayManager, DisplayManager>();
        }

        public IDataFilesManager GetDataFilesManager()
        {
            return container.Resolve<IDataFilesManager>();
        }

        public IServerRequestsManager GetServerRequestsManager()
        {
            return container.Resolve<IServerRequestsManager>();
        }

        public IDisplayManager GetDisplayManager()
        {
            return container.Resolve<IDisplayManager>();
        }
    }
}
