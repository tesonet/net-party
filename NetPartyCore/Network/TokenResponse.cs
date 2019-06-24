using System.Runtime.Serialization;

namespace NetPartyCore.Network
{
    [DataContract]
    internal class TokenResponse
    {
        [DataMember]
        internal string token;
    }
}
