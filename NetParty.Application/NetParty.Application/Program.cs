#region Using

using Autofac;
using Serilog;

#endregion

namespace NetParty.Application
    {
    internal class Program
        {
        private static void Main(string[] args)
            {
            using (var scope = DependencyContainer.Container.BeginLifetimeScope())
                {
                Log.Logger = scope.Resolve<ILogger>();
                }
            }
        }
    }
