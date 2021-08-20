using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace partycli.Entities
{
    [Verb("server_list")]
    public class ServerOptions
    {
        [Option("local")]
        public bool Local { get; set; }
    }
}
