using Newtonsoft.Json;

namespace partycli
{
    public class Server
    {
        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("distance")]
        public int Distance { get; private set; }

        public Server(string name, int distance)
        {
            Name = name;
            Distance = distance;
        }
    }
}
