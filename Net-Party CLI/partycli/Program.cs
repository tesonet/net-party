using log4net;
using partycli.Servers;
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

                string username = "tesonet";
                string password = "partyanimal";

                var repository = container.Resolve<IAuthenticationRepository>();
                repository.SaveCredentialsAsync(username, password).Wait();


                var serversHandler = container.Resolve<ServersHandler>();                
                serversHandler.GetServersListAsync(repository.RetrieveToken()).Wait(); 
                serversHandler.GetServersListLocal().Wait();

                //TODO: use command line arguments as parameters and options to control application flow
                //TODO: pretty printing
                //TODO: error handling
            };
        }
    }
}
