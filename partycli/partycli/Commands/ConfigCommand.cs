using CommandLine;

namespace partycli.Commands
{
    [Verb("config", HelpText = "Sets username and password.")]
    public class ConfigCommand : ICommand
    {
        [Option("username", Required = true, HelpText = "Username to be set.")]
        public string Username { get; set; }

        [Option("password", Required = true, HelpText = "Password to be set.")]
        public string Password { get; set; }
    }
}
