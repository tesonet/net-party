using CommandLine;

﻿namespace NetParty.App.ConsoleOptions
{
    [Verb("config", HelpText = "Store username and password for API authorization in the persistent data store")]
    class ConfigOptions
    {
        [Option("username", Required = true, HelpText = "Username")]
        public string Username { get; set; }
        [Option("password", Required = true, HelpText = "Password")]
        public string Password { get; set; }
    }
}