using Microsoft.Extensions.Configuration;
using PartyCli.Core.Entities;
using PartyCli.Core.Interfaces;

namespace PartyCli.Infrastructure.Repositories
{
    public class ServersFileRepository : GenericFileRepository<Server>, IServersRepository
    {
        public ServersFileRepository(ILogger logger, IConfiguration configuration) : base(logger, configuration)
        { }
    }
}
