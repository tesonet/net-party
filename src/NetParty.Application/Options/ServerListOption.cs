using CommandLine;

namespace NetParty.Application.Options
{
    [Verb("server_list", HelpText = "Fetch servers from Tesonet API, store them in persistent data store and display server names and total number of servers")]
    public class ServerListOption
    {
        [Option("local", Required = false, HelpText = "Fetch servers from persistent data store and display server names and total number of servers")]
        public bool Local { get; set; }
    }
}
