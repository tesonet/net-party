using RestSharp.Deserializers;

namespace net_party.Entities.API
{
    public class TokenResponse
    {
        [DeserializeAs(Name = "token")]
        public string Token { get; set; }
    }
}