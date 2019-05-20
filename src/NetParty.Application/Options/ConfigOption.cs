using CommandLine;

namespace NetParty.Application.Options
{
    [Verb("config")]
    public class ConfigOption
    {
        [Option("username", Required = true, HelpText = "Set YOUR USERNAME from tesonet")]
        public string UserName { get; set; }


        [Option("password", Required = true, HelpText = "Set YOUR PASSWORD from tesonet")]
        public string Password { get; set; }
    }
}
