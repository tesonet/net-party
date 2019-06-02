#region Using

using Autofac;
using NetParty.Application.DependencyInjection.Modules;

#endregion

namespace NetParty.Application.DependencyInjection
    {
    public static class DependencyContainer
        {
        public static readonly IContainer Container = BuildContainer();

        private static IContainer BuildContainer()
            {
            var builder = new ContainerBuilder();

            builder.RegisterModule<LoggerModule>();
            builder.RegisterModule<CredentialsModule>();

            return builder.Build();
            }
        }
    }
