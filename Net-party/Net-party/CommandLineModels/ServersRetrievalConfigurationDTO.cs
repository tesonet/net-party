using CommandLine;

namespace Net_party.CommandLineModels
{

    [Verb("server_list", HelpText = "Displays first lines of a file.")]
    public class ServersRetrievalConfigurationDto
    {
        [Option("local", Required = false, HelpText = "Set output to verbose messages.")]
        public bool IsLocal { get; set; }

        public override string ToString()
        {
            return $"{nameof(IsLocal)}: {IsLocal}";
        }
    }
}
