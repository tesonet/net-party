using CommandLine;

namespace PartyCLI
{
    [Verb("config", HelpText = "Config username and password for authentication")]
    public class UserData
    {
        [Option('u', "username", Required = true, HelpText = "Username for authentication")]
        public string Username { get; set; }
        [Option('p', "password", Required = true, HelpText = "Password for authentication")]
        public string Password { get; set; }
    }
}
