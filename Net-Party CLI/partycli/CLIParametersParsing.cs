using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Commander.NET.Attributes;

namespace partycli.Options
{
    public class APIConfig
    {
        [Parameter("--username")]
        public string username;
        [Parameter("--password")]
        public string password;
    }

    public class APIListServers
    {
        [Parameter("--local", Required = Required.No)]
        public Local local;
    }
    public class Local { //need a flag attribute instead 
    };
    public class CLIOptions
    {
        [Command("config")]
        public APIConfig config;

        [Command("server_list")]
        public APIListServers server_list;
    }
}
