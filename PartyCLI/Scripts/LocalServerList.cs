using CommandLine;

namespace PartyCLI
{
    [Verb("server_list", HelpText = "Get and display server list")]
    public class LocalServerList
    {
        [Option("local", Required = false, HelpText = "Toggle if servers should be retrieved from local storage")]
        public bool ShowLocal { get; set; }
    }
}
