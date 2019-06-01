#region Using

using Autofac;
using Serilog;

#endregion

namespace NetParty.Application
    {
    public static class DependencyContainer
        {
        public static readonly IContainer Container = BuildContainer();

        private static IContainer BuildContainer()
            {
            var builder = new ContainerBuilder();

            builder.RegisterInstance<ILogger>(new LoggerConfiguration().MinimumLevel.Debug().WriteTo.Console().CreateLogger());

            return builder.Build();
            }
        }
    }
