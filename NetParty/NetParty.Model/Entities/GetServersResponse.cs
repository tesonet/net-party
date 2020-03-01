using System.Collections.Generic;

namespace NetParty.Model.Entities
{
    public class GetServersResponse : ResponseBase
    {
        public List<Server> Servers { get; set; }
    }
}