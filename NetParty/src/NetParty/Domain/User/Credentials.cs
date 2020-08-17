using Newtonsoft.Json;

namespace NetParty.Domain.User
{
    public class Credentials
    {
        [JsonProperty("username")]
        public string UserName { get; }

        [JsonProperty("password")]
        public string Password { get; }

        public Credentials(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
