using System.Runtime.Serialization;

namespace PartyCli.WebApiClient.DataContracts
{
  [DataContract(Name = "server")]
  public class ServerDataContract
  {
    [DataMember(Name = "name")]
    public string Name { get; set; }
    [DataMember(Name = "distance")]
    public int Distance { get; set; }
  }
}
