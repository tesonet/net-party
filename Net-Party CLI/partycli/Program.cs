using log4net;
using Unity;

namespace partycli
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var container = DependencyContainer.container)
            {
                var log = container.Resolve<ILog>();
            };
        }
    }
}
