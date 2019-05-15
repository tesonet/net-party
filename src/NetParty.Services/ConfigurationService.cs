using System.Threading.Tasks;
using NetParty.Contracts;
using NetParty.Contracts.Exceptions;
using NetParty.Interfaces.Repositories;
using NetParty.Interfaces.Services;

namespace NetParty.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly ICredentialsRepository _credentialsRepository;
        private readonly IValidationService _validationService;
        private readonly IOutputService _outputService;

        public ConfigurationService(ICredentialsRepository credentialsRepository,
            IValidationService validationService, IOutputService outputService)
        {
            _credentialsRepository = credentialsRepository;
            _validationService = validationService;
            _outputService = outputService;
        }

        public Task StoreCredentials(Credentials credentials)
        {
            if (!_validationService.Validate(credentials).IsValid)
                throw new BaseValidationException("Invalid Credentials!");

            _outputService.OutputStringLine("Storing credentials");

            return _credentialsRepository.SaveCredentials(credentials);
        }
    }
}