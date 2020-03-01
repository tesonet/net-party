using Newtonsoft.Json;

namespace NetParty.Model.Entities
{
    public class Credentials
    {
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}