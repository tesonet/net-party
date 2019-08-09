using Newtonsoft.Json;
using partycli.Http;
using partycli.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace partycli.Servers
{
    public class ServersRepository: IServersRepository
    {
        IHttpService m_httpService = null;
        IRepositoryProvider m_serversRepositoryProvider = null;
        public ServersRepository(IHttpService httpService, IRepositoryProvider serversRepositoryProvider)
        {
            m_httpService = httpService;
            m_serversRepositoryProvider = serversRepositoryProvider;
        }

        public async Task<List<Server>> RetrieveServersListAsync(string token)
        {
            //get from http
            var server_list = await m_httpService.GetWithToken(token);
            //store in persistant
            m_serversRepositoryProvider.SaveAsync(server_list).Wait();
            //return List
            return JsonConvert.DeserializeObject<List<Server>>(server_list);
        }

        public async Task<List<Server>> RetrieveServersListLocalAsync()
        {
            //get from persistant
            var server_list = await m_serversRepositoryProvider.LoadAsync();
            //return List
            return JsonConvert.DeserializeObject<List<Server>>(server_list);
        }
    }
}
