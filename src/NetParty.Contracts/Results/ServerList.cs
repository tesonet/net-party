using System.Collections.Generic;
using NetParty.Domain.Models;

namespace NetParty.Contracts.Results
{
    public class ServerList
    {
        public List<Server> Items { get; set; }
        public int Count { get; set; }
    }
}