using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
