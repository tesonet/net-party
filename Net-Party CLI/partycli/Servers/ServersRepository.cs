using Newtonsoft.Json;
using partycli.Helpers;
using partycli.Http;
using partycli.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace partycli.Servers
{
    public class ServersRepository : IServersRepository
    {
        IHttpService m_httpService = null;
        IRepositoryProvider m_serversRepositoryProvider = null;
        public ServersRepository(IHttpService httpService, IRepositoryProvider serversRepositoryProvider)
        {
            m_httpService = httpService;
            m_serversRepositoryProvider = serversRepositoryProvider;
        }

        public async Task<IRequestResult<List<Server>>> RetrieveServersListAsync(string token)
        {
            var response = await m_httpService.GetWithToken(token);
            if (response.Success)
            {
                var server_list = response.Result;
                m_serversRepositoryProvider.SaveAsync(server_list).Wait();
                return new SuccessResult<List<Server>>(JsonConvert.DeserializeObject<List<Server>>(server_list));
            }
            return new FailedResult(response.ErrorMessage) as IRequestResult<List<Server>>;
        }

        public async Task<List<Server>> RetrieveServersListLocalAsync()
        {
            var server_list = await m_serversRepositoryProvider.LoadAsync();
            return JsonConvert.DeserializeObject<List<Server>>(server_list);            
        }
    }
}
