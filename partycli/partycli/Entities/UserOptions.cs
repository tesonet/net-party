using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace partycli.Entities
{
    [Verb("config")]
    public class UserOptions
    {       
        [Option("password")]
        public string Password { get; set; }
        [Option("username")]
        public string Username { get; set; }
    }
}
