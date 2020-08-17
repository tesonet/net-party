using Newtonsoft.Json;

namespace NetParty.Domain.Servers
{
    public class Server
    {
        [JsonProperty("name")]
        public string Name { get; }

        public Server(string name)
        {
            Name = name;
        }
    }
}
