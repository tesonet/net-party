using Newtonsoft.Json;
using System;

namespace partycli
{
    public class Credentials
    {
        [JsonProperty("username")]
        public string Username { get; private set; }

        [JsonProperty("password")]
        public string Password { get; private set; }

        public Credentials(string username, string password)
        {
            Username = username;
            Password = password;
        }


    }
}
