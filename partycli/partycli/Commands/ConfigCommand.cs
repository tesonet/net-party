using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using partycli.core;

namespace partycli.Commands
{
    [Verb("config", HelpText = "Sets username and password.")]
    class ConfigCommand : ICommand
    {
        [Option("username", Required = true, HelpText = "Username to be set.")]
        public string Username { get; set; }

        [Option("password", Required = true, HelpText = "Password to be set.")]
        public string Password { get; set; }
    }
}
