using System.Runtime.Serialization;

namespace PartyCli.WebApiClient.DataContracts
{
  [DataContract(Name = "token")]
  public class TokenDataContract
  {
    [DataMember(Name = "token")]
    public string Token { get; set; }
  }
}
