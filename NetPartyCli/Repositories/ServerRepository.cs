using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetPartyCli.Database;

namespace NetPartyCli.Repositories
{
    public class ServerRepository
    {
        private readonly PartyContext _context;

        public ServerRepository(PartyContext context)
        {
            _context = context;
        }

        public async Task<IList<Server>> GetAllAsync()
        {
            return await _context.Servers.ToListAsync();
        }

        public async Task SaveAsync(IEnumerable<Server> servers)
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
