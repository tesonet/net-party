using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using partycli.Contracts.DTOs;
using partycli.Contracts.Entities;
using partycli.Contracts.Repositories;
using partycli.Contracts.Services;

namespace partycli.Services
{
    public class ConfigurationLedger : IConfigurationLedger
    {
        private readonly IConfigurationRepository _configurationRepository;
        private readonly ILogger<ConfigurationLedger> _logger;

        public ConfigurationLedger(IConfigurationRepository configurationRepository, ILogger<ConfigurationLedger> logger)
        {
            _configurationRepository = configurationRepository;
            _logger = logger;
        }

        public async Task AddOrUpdateAsync(ConfigurationDTO configurationDto)
        {
            var configurationEntity = await _configurationRepository.GetAsync();

            if (configurationEntity == null)
            {
                await AddAsync(configurationDto);
                return;
            }

            await UpdateAsync(configurationDto, configurationEntity);
        }

        private Task UpdateAsync(ConfigurationDTO configurationDto, ConfigurationEntity configurationEntity)
        {
            _logger.LogDebug("Updating existing configuration.");
            configurationEntity.Password = configurationDto.Password;
            configurationEntity.Username = configurationDto.Username;
            return _configurationRepository.UpdateAsync(configurationEntity);
        }

        private Task AddAsync(ConfigurationDTO configurationDto)
        {
            _logger.LogDebug("Adding configuration.");

            return _configurationRepository.SaveAsync(new ConfigurationEntity
            {
                Password = configurationDto.Password,
                Username = configurationDto.Username
            });
        }

        public async Task<ConfigurationDTO> GetAsync()
        {
            _logger.LogDebug("Getting configuration.");
            var configurationEntity = await _configurationRepository.GetAsync();

            return new ConfigurationDTO(configurationEntity.Username, configurationEntity.Password);
        }
    }
}
