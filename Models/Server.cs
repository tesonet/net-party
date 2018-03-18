using Newtonsoft.Json;

namespace Tesonet
{
    public class Server
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("distance")]
        public int Distance { get; set; }
    }
}
