using CommandLine;

namespace Net_party.CommandLineModels
{
    [Verb("config", HelpText = "Displays first lines of a file.")]
    public class CredentialsDto
    {
        [Option("username", Required = true, HelpText = "Set output to verbose messages.")]
        public string Username { get; set; }

        [Option("password", Required = true, HelpText = "Set output to verbose messages.")]
        public string Password { get; set; }

        public override string ToString()
        {
            return $"{nameof(Username)}: {Username}, {nameof(Password)}: {Password}";
        }

    }
}
