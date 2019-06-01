namespace NetParty.Application
    {
    internal class Program
        {
        private static void Main(string[] args)
            {
            using (var scope = DependencyContainer.Container.BeginLifetimeScope()) { }
            }
        }
    }
