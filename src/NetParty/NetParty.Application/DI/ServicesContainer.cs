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

            builder
                .Register(c => 
                    new LoggerConfiguration() 
                    .MinimumLevel.Debug()
                    .WriteTo.ColoredConsole()
                    .CreateLogger())
                .As<ILogger>()
                .SingleInstance();

            Container = builder.Build();
        }
    }
}
