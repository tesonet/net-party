using System.Threading.Tasks;
using partycli.Contracts.Entities;

namespace partycli.Contracts.Repositories
{
    public interface IConfigurationRepository
    {
        Task SaveAsync(ConfigurationEntity configurationEntity);
        Task<ConfigurationEntity> GetAsync();
        Task UpdateAsync(ConfigurationEntity configurationEntity);
    }
}
