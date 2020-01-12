using RestSharp.Deserializers;

namespace net_party.Entities.API
{
    public class ServerListResponse
    {
        [DeserializeAs(Name = "name")]
        public string Name { get; set; }

        [DeserializeAs(Name = "distance")]
        public long Distance { get; set; }
    }
}
