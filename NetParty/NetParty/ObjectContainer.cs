using SimpleInjector;

namespace NetParty
{
    public static class ObjectContainer
    {
        private static Container _container;

        public static void Init()
        {
            _container = new Container();

            Model.ObjectContainerInitializer.Init(_container);

            _container.Verify();
        }

        public static T GetInstance<T>() where T : class
        {
            if (_container == null)
                return null;
            return _container.GetInstance<T>();
        }
    }
}