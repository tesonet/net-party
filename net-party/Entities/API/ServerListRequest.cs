using net_party.Entities.API.Base;

namespace net_party.Entities.API
{
    public class ServerListRequest : IAuthenticated
    {
        public string AuthToken { get; set; }
    }
}