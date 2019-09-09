using CommandLine;


namespace NetPartyCli.Presentation
{
    [Verb("server_list", HelpText = "Fetches server list from API and saves data to persistent data store.")]
    public class ServerOption
    {
        [Option('l', "local", Required = false, HelpText = "Fetches data from persistent data store.")]
        public bool Local { get; set; }
    }
}