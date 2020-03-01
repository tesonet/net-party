using CommandLine;

namespace NetParty.Entities
{
    [Verb("config")]
    public class ConfigOptions
    {
        [Option("username")]
        public string Username { get; set; }
        [Option("password")]
        public string Password { get; set; }
    }
}