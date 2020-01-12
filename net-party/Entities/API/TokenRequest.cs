using Newtonsoft.Json;

namespace net_party.Entities.API
{
    public class TokenRequest
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        public TokenRequest() { }

        public static TokenRequest New(string username, string password)
        {
            return new TokenRequest()
            {
                Username = username,
                Password = password
            };
        }
    }
}