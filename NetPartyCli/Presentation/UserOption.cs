using CommandLine;

namespace NetPartyCli.Presentation
{
    [Verb("config", HelpText = "Stores username and password for API authorization in the persistent data store.")]
    public class UserOption
    {
        [Option('u', "username", Required = false, HelpText = "API username.")]
        public string Username { get; set; }

        [Option('p', "password", Required = false, HelpText = "API password.")]
        public string Password { get; set; }
    }
}