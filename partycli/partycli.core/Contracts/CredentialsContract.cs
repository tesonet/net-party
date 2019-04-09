using System.Runtime.Serialization;

namespace partycli.core.Contracts
{
    [DataContract(Name = "Credentials")]
    public class CredentialsContract
    {
        [DataMember(Name = "username")]
        public string Username { get; set; }
        [DataMember(Name = "password")]
        public string Password { get; set; }
    }
}
