#region Using

using CommandLine;

#endregion

namespace NetParty.Application.CommandLineOptions
    {
    [Verb("server_list", HelpText = "List servers. Default option will cache server list locally.")]
    public class ServerListOptions
        {
        [Option("local", Default = false, Required = false, HelpText = "Will show cached server only and will not update the list")]
        public bool Local { get; set; }
        }
    }
