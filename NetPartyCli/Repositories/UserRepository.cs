using System.Threading.Tasks;
using NetPartyCli.Database;

namespace NetPartyCli.Repositories
{
    public class UserRepository
    {
        private readonly PartyContext _context;

        public UserRepository(PartyContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(User tesonetUserDto)
        {
            await _context.Users.AddAsync(tesonetUserDto);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetAsync()
        {
            return await _context.Users.FindAsync(1);
        }
    }
}
