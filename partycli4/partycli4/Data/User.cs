
using CommandLine;

namespace partycli4.Data
{
    public class User
    {
        [Option("username", Required = true)]
        public string Username { get; set; }
        [Option("password", Required = true)]
        public string Password { get; set; }
    }
}
