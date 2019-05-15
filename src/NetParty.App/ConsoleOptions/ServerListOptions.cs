using CommandLine;

﻿namespace NetParty.App.ConsoleOptions
{
    [Verb("server_list", HelpText = "Display server list")]
    class ServerListOptions
    {
        [Option("local", Required = false, Default = false, HelpText = "Use local data store, else fetch from API.")]
        public bool Local { get; set; }
    }
}