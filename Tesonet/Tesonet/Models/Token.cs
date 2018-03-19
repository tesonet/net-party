using Newtonsoft.Json;

namespace Tesonet
{
    public class Token
    {
        [JsonProperty("token")]
        public string Value { get; set; }
    }
}
