using NetParty.Contracts.Requests.Base;

namespace NetParty.Contracts.Requests
{
    public class ConfigurationRequest : BaseRequest
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
