using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using partycli.Contracts.Entities;
using partycli.Contracts.Repositories;

namespace partycli.DataAccess
{
    public class ServerRepository : IServerRepository
    {
        private readonly PartyContext _context;

        public ServerRepository(PartyContext context)
        {
            _context = context;
        }

        public async Task<IList<ServerEntity>> GetAllAsync()
        {
            return await _context.Servers.ToListAsync();
        }

        public async Task SaveAsync(IEnumerable<ServerEntity> servers)
        {
            await _context.Servers.AddRangeAsync(servers);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAllAsync()
        {
            _context.Servers.RemoveRange(_context.Servers);
            await _context.SaveChangesAsync();
        }
    }
}
