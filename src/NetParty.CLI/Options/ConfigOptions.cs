using CommandLine;

namespace NetParty.CLI.Options
{
    [Verb(Constants.Verbs.Config.Name, HelpText = Constants.Verbs.Config.HelpText)]
    public class ConfigOptions
    {
        [Option(Constants.Verbs.Config.Options.Username.Name, 
                HelpText = Constants.Verbs.Config.Options.Username.HelpText, 
                Required = true)]
        public string Username { get; set; }
        [Option(Constants.Verbs.Config.Options.Password.Name, 
                HelpText = Constants.Verbs.Config.Options.Password.HelpText, 
                Required = true)]
        public string Password { get; set; }
    }
}
