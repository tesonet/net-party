using CommandLine;

namespace partycli.Commands
{
    [Verb("server-list", HelpText = "Fetch server list.")]
    public class ServerListCommand : ICommand
    {
        [Option("local", Required = false, HelpText = "Fetch list from local storage.", Default = false)]
        public bool FetchLocal { get; set; }
    }
}
