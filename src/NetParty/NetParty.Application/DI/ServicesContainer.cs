using Autofac;
using Serilog;

namespace NetParty.Application.DI
{
    public static class ServicesContainer
    {
        public static IContainer Container;

        static ServicesContainer()
        {
            CreateContainer();
        }

        private static void CreateContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<LogModule>();
            builder.RegisterModule<HandlersModule>();
            builder.RegisterModule<ServicesModule>();

            Container = builder.Build();
        }
    }
}
