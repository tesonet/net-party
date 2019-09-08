using CommandLine;

namespace partycli.Contracts.Verbs
{
    [Verb("config", HelpText = "Stores username and password for API authorization in the persistent data store.")]
    public class ConfigurationVerb
    {
        [Option('u', "username", Required = false, HelpText = "API username.")]
        public string Username { get; set; }

        [Option('p', "password", Required = false, HelpText = "API password.")]
        public string Password { get; set; }
    }
}