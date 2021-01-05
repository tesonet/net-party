namespace Tesonet.ServerListApp.Application
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Domain;

    public class ServersList
    {
        private readonly IServersListClient _serversListClient;
        private readonly IServersRepository _repository;

        public ServersList(IServersListClient serversListClient, IServersRepository repository)
        {
            _serversListClient = serversListClient;
            _repository = repository;
        }

        public async Task<IEnumerable<Server>> GetLatest()
        {
            var servers = await _serversListClient.GetAll();
            await _repository.Store(servers);

            var ordered = servers
                .Select(s => new Server(s.Name, s.Distance))
                .OrderBy(s => s.Name);

            return ordered;
        }

        public async Task<IEnumerable<Server>> GetCached()
        {
            var servers = await _repository.GetAll();

            var ordered = servers
                .Select(s => new Server(s.Name, s.Distance))
                .OrderBy(s => s.Name);

            return ordered;
        }
    }
}