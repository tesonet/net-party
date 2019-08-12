using partycli.Config;
using partycli.Helpers;
using partycli.Servers;
using System.Threading.Tasks;

namespace partycli.Api
{
    class ApiHandler
    {
        IAuthenticationRepository m_authRepository = null;
        IServersRepository m_serversService = null;
        IPrinter m_print = null;

        public ApiHandler(IAuthenticationRepository authRepository, IServersRepository serversService, IPrinter printer)
        {
            m_authRepository = authRepository;
            m_serversService = serversService;
            m_print = printer;
        }
        internal void SaveCredentials(string username, string password)
        {
            m_authRepository.SaveCredentialsAsync(username, password).Wait();
            m_print.Info("User configuration was successful");
        }

        public async Task GetServersListAsync()
        {
            var tokenResponse = await m_authRepository.RetrieveToken();            
            if (!tokenResponse.Success)
            {
                m_print.Error(tokenResponse.ErrorMessage);
                return;
            }
            var response = await m_serversService.RetrieveServersListAsync(tokenResponse.Result);
            if (!response.Success)
                m_print.Error(response.ErrorMessage);
            m_print.ServersList(response.Result);  
        }

        public async Task GetServersListLocalAsync()
        {
            var servers_list = await m_serversService.RetrieveServersListLocalAsync();
            m_print.ServersList(servers_list);
        }
    }
}