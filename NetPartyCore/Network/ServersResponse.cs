using System.Runtime.Serialization;

namespace NetPartyCore.Network
{
    [DataContract]
    internal class ServersResponse
    {
        [DataMember]
        internal string name;

        [DataMember]
        internal int distance;
    }
}
