using System.Runtime.Serialization;

namespace partycli.core.Contracts
{
    [DataContract(Name = "server")]
    public class ServerContract
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "distance")]
        public int Distance { get; set; }
    }
}
