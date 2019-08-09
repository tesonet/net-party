using partycli.Config;
using partycli.Servers;
using System;
using System.Threading.Tasks;

namespace partycli.Api
{
    class ApiHandler
    {
        IAuthenticationRepository m_authRepository = null;
        IServersRepository m_serversService = null;

        public ApiHandler(IAuthenticationRepository authRepository, IServersRepository serversService)
        {
            m_authRepository = authRepository;
            m_serversService = serversService;
        }
        internal void SaveCredentials(string username, string password)
        {
            m_authRepository.SaveCredentialsAsync(username, password).Wait();
            Console.WriteLine("authentication were configured successfully");
        }

        public async Task GetServersListAsync()
        {
            var tokenResponse = await m_authRepository.RetrieveToken();
            if (tokenResponse.Success)
            {
                var response = await m_serversService.RetrieveServersListAsync(tokenResponse.Result);
                if (response.Success)
                    foreach (var server in response.Result)
                        Console.WriteLine(server.Name + " " + server.Distance);
                else
                    Console.WriteLine(response.ErrorMessage);
            }
            else
            {
                Console.WriteLine(tokenResponse.ErrorMessage);
            }
            Console.ReadKey();
        }

            public async Task GetServersListLocalAsync()
        {
            var serves_list = await m_serversService.RetrieveServersListLocalAsync();
            foreach (var server in serves_list)
                Console.WriteLine(server.Name + " " + server.Distance);            
            Console.ReadKey();
        }
    }
}