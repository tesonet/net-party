namespace Tesonet.ServerListApp.Infrastructure.Storage
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain;
    using Microsoft.EntityFrameworkCore;

    internal class ServersRepository : IServersRepository
    {
        private readonly ServersDbContext _serversDbContext;

        public ServersRepository(ServersDbContext serversDbContext)
        {
            _serversDbContext = serversDbContext;
        }

        public async Task<IEnumerable<Server>> GetAll()
        {
            return await _serversDbContext.Servers.ToListAsync();
        }

        public async Task Store(IEnumerable<Server> servers)
        {
            await _serversDbContext.Truncate<Server>();
            await _serversDbContext.AddRangeAsync(servers);
            await _serversDbContext.SaveChangesAsync();
        }
    }
}