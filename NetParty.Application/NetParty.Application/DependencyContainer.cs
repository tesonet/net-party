#region Using

using Autofac;

#endregion

namespace NetParty.Application
    {
    public static class DependencyContainer
        {
        public static readonly IContainer Container = BuildContainer();

        private static IContainer BuildContainer() => new ContainerBuilder().Build();
        }
    }
