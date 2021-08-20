
using partycli.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace partycli.Models.Service
{
   public class GetServersListResponse : Entities.ResponseBase
    {
     public    List<ServerList> Servers { get; set; }
    }
}
