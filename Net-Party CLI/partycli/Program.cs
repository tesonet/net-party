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

                string username = "tesonet";
                string password = "partyanimal";

                var repository = container.Resolve<IAuthenticationRepository>();
                repository.SaveCredentialsAsync(username, password).Wait();

                //TODO: list servers (server_list)
                //TODO: list servers from local storage (server_list --local)

                //TODO: use command line arguments as parameters and options to control application flow
            };
        }
    }
}
