using NetParty.Application.Handlers.Base;
using NetParty.Application.Options;
using System;
using System.Threading.Tasks;
using System.Security.Cryptography;
using NetParty.Application.Utils;

namespace NetParty.Application.Handlers
{
    public class ConfigHandler : BaseHandler<ConfigOption>
    {
        private readonly ICredentialsService _credentialsService;

        public ConfigHandler(ICredentialsService credentialsService)
        {
            _credentialsService = credentialsService;
        }

        public override Task HandleBaseAsync(ConfigOption request)
        {
            return _credentialsService.SaveCredentialsAsync(request.UserName, request.Password);
        }
    }
}
