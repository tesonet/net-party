using System.Runtime.Serialization;

namespace partycli.core.Contracts
{
    [DataContract(Name = "token")]
    class TokenContract
    {
        [DataMember(Name = "token")]
        public string Token { get; set; }
    }
}
