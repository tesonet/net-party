using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace partycli.Entities
{
    public class GetServersListResponse : ResponseBase
    {
        public List<ServerList> Servers { get; set; }
    }
}
