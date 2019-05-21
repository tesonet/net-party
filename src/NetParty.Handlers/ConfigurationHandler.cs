using System.Threading.Tasks;
using GuardNet;
using NetParty.Contracts;
using NetParty.Handlers.Base;
using NetParty.Contracts.Requests;
using NetParty.Repositories.Core;
using NetParty.Services.Interfaces;

namespace NetParty.Handlers
{
    public class ConfigurationHandler : BaseHandler<ConfigurationRequest>
    {
        private readonly ICredentialsRepository _credentialsRepository;

        public ConfigurationHandler(ICredentialsRepository credentialsRepository)
        {
            Guard.NotNull(credentialsRepository, nameof(credentialsRepository));

            _credentialsRepository = credentialsRepository;
        }

        public override Task HandleBaseAsync(ConfigurationRequest request)
        {
            var credentials = new Credentials
            {
                UserName = request.UserName,
                Password = request.Password
            };

            return _credentialsRepository.SaveCredentialsAsync(credentials);
        }
    }
}
