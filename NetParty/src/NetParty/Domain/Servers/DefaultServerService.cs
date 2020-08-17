using System.Collections.Generic;
using System.Threading.Tasks;
using NetParty.Data;

namespace NetParty.Domain.Servers
{
    public class DefaultServerService : IServerService
    {
        private readonly IReadOnlyRepository<ICollection<Server>> _sourceRepository;
        private readonly IRepository<ICollection<Server>> _defaultRepository;

        public DefaultServerService(
            IReadOnlyRepository<ICollection<Server>> sourceRepository,
            IRepository<ICollection<Server>> defaultRepository)
        {
            _sourceRepository = sourceRepository;
            _defaultRepository = defaultRepository;
        }

        public async Task<ICollection<Server>> GetAll(bool refresh)
        {
            if (!refresh)
            {
                var saved = await _defaultRepository.GetAsync();
                return saved ?? new List<Server>();
            }

            var sourceServers = await _sourceRepository.GetAsync();

            await _defaultRepository.SaveAsync(sourceServers);

            return sourceServers;
        }
    }
}
