#region Using

using System;
using Newtonsoft.Json;

#endregion

namespace NetParty.Application.CredentialsNS
    {
    public class Credentials : IEquatable<Credentials>
        {
        [JsonConverter(typeof(EncryptingJsonConverter),
            "P%Zq-8&hVS3KRczspg&a%Qj$DJ&EH6n&cZt_BW&k4cGZtgP=z?WRmx6XZAkC-JJThcN_fwS6N6ngnzXnw22EugKBaQ2a@XNk*$k_znAnnHR_c6PCm_E5m!AUJ9fyeS8%2ZQcnrkf5ZF74-+dZ!_QkSJpQ=AacKvGhZkucxSY-nZrRSqNWJ*f*z&cQJnf$9d=7k3eFqCkDJQvshHzMwt$DmCCR#z@bb-t^&%kexsbff#5zkgsVn5B3+c2Bu35?dxj")]
        public string Username { get; set; }

        [JsonConverter(typeof(EncryptingJsonConverter),
            "tJCTUZv784kf-8UgJJGLpZu&ap@e?p4%-yT#XDNd^#!AHgmdjyd%z$XZCN+DJJH#gVc&v84Hb+g82cZaK^4!!fX33tb$m8R+hX!+hEfp@KgZ=#@Cdw!2xBtm@U!x#$68JrXQTv=3RT8qH6SY?dnZ?=5q4#YSjR9Lj&gqrLcFK46m$wCkSah=7e%7XqyZn%&e3NvEq5Mr2EH9qn!dsBhEVLrx5Je2zS@uFvaeaJ$bgt9Hp%6=Jm#uAW9qDVuR#47W")]
        public string Password { get; set; }

        public static bool operator ==(Credentials left, Credentials right)
            {
            return Equals(left, right);
            }

        public static bool operator !=(Credentials left, Credentials right)
            {
            return !Equals(left, right);
            }

        public bool Equals(Credentials other)
            {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return string.Equals(Username, other.Username) && string.Equals(Password, other.Password);
            }

        public override bool Equals(object obj)
            {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;
            return Equals((Credentials) obj);
            }
        }
    }
