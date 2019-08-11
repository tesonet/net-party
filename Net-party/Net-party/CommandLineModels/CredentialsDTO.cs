using CommandLine;

namespace Net_party.CommandLineModels
{
    [Verb("config", HelpText = "Saves credential information in the permanent storage.")]
    public class CredentialsDto
    {
        [Option("username", Required = true, HelpText = "Username that's going to be sent to the api.")]
        public string Username { get; set; }

        [Option("password", Required = true, HelpText = "Password that's going to be sent to the api.")]
        public string Password { get; set; }

        public override string ToString()
        {
            return $"{nameof(Username)}: {Username}, {nameof(Password)}: {Password}";
        }

    }
}
