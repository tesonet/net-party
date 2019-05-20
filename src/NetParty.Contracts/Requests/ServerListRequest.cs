using NetParty.Contracts.Requests.Base;

namespace NetParty.Contracts.Requests
{
    public class ServerListRequest : BaseRequest
    {
        public bool Local { get; set; }
    }
}
