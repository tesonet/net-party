using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using partycli.core;
using partycli.core.DataAccess;

namespace partycli.Commands
{
    [Verb("server-list", HelpText = "Fetch server list.")]
    class ServerListCommand : ICommand
    {
        [Option("local", Required = false, HelpText = "Fetch list from local storage.", Default = false)]
        public bool FetchLocal { get; set; }
    }
}
