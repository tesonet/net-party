#region Using

using System;
using Newtonsoft.Json;

#endregion

namespace NetParty.Application.Servers
    {
    public class Server : IEquatable<Server>
        {
        public Server(string name, uint distance)
            {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Distance = distance;
            }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; }

        [JsonProperty(PropertyName = "distance")]
        public uint Distance { get; }

        public static bool operator ==(Server left, Server right)
            {
            return Equals(left, right);
            }

        public static bool operator !=(Server left, Server right)
            {
            return !Equals(left, right);
            }

        public bool Equals(Server other)
            {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return string.Equals(Name, other.Name) && Distance == other.Distance;
            }

        public override bool Equals(object obj)
            {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;
            return Equals((Server) obj);
            }

        public override int GetHashCode()
            {
            unchecked
                {
                return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ (int) Distance;
                }
            }

        public override string ToString() => $"Server: {Name}({Distance})";
        }
    }
