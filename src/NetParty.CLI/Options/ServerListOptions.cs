using CommandLine;

namespace NetParty.CLI.Options
{
    [Verb(Constants.Verbs.ServerList.Name, HelpText = Constants.Verbs.ServerList.HelpText)]
    public class ServerListOptions
    {
        [Option(Constants.Verbs.ServerList.Options.Local.Name,
            HelpText = Constants.Verbs.ServerList.Options.Local.HelpText)]
        public bool Local { get; set; }
    }
}