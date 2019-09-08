using System.Threading.Tasks;
using partycli.Contracts.Entities;
using partycli.Contracts.Repositories;

namespace partycli.DataAccess
{
    public class ConfigurationRepository: IConfigurationRepository
    {
        private readonly PartyContext _context;

        public ConfigurationRepository(PartyContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(ConfigurationEntity configurationDto)
        {
            await _context.Configuration.AddAsync(configurationDto);
            await _context.SaveChangesAsync();
        }

        public async Task<ConfigurationEntity> GetAsync()
        {
            return await _context.Configuration.FindAsync(1);
        }

        public async Task UpdateAsync(ConfigurationEntity configurationEntity)
        {
            _context.Configuration.Update(configurationEntity);
            await _context.SaveChangesAsync();
        }
    }
}
