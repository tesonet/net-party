using CommandLine;
using NetParty.Model.Entities.Enums;

namespace NetParty.Entities
{
    [Verb("server_list")]
    public class ServerListOptions
    {
        [Option("local")]
        public bool Local { get; set; }
        [Option("order")]
        public Order Order { get; set; }
    }
}