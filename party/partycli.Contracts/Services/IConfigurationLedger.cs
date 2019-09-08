using System.Threading.Tasks;
using partycli.Contracts.DTOs;

namespace partycli.Contracts.Services
{
    public interface IConfigurationLedger
    {
        Task AddOrUpdateAsync(ConfigurationDTO configurationDto);
        Task<ConfigurationDTO> GetAsync();
    }
}
