using Autofac;
using NetParty.CLI.DI;

namespace NetParty.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var containerBuilder = new ContainerBuilder();
            RegisterDependencies(containerBuilder);
            var container = containerBuilder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                var application = scope.Resolve<Application>();
                application.Run(args);
            }
        }

        static void RegisterDependencies(ContainerBuilder builder)
        {
            builder.RegisterType<Application>();
            builder.RegisterModule<UtilModule>();
            builder.RegisterModule<ResultsPrinterModule>();
            builder.RegisterModule<InfrastructureModule>();
            builder.RegisterModule<ControllersModule>();
            builder.RegisterModule<HandlersModule>();
        }
    }
}