using log4net;
using partycli.Api;
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
                log.Info("Net-Party CLI");

                string username = "tesonet";
                string password = "partyanimal";
                
                var serversHandler = container.Resolve<ApiHandler>();

                serversHandler.SaveCredentials(username, password);
                serversHandler.GetServersListAsync().Wait();
                serversHandler.GetServersListLocalAsync().Wait();

                //TODO: use command line arguments as parameters and options to control application flow
            };
        }
    }
}
