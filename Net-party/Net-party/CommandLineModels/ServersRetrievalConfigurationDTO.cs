using CommandLine;

namespace Net_party.CommandLineModels
{

    [Verb("server_list", HelpText = "Displays available servers and the distance from them.")]
    public class ServersRetrievalConfigurationDto
    {
        [Option("local", Required = false, HelpText = "If this parameter is set, the data is going to be aggregated from local storage.")]
        public bool IsLocal { get; set; }

        public override string ToString()
        {
            return $"{nameof(IsLocal)}: {IsLocal}";
        }
    }
}
