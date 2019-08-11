using CommandLine;

namespace partycli.Options
{
    [Verb("server_list", HelpText = "List servers")]
    class ServerListSubOptions
    {
        [Option("local", HelpText = "Use list from persistant storage")]
        public bool Local { get; set; }
    }

    [Verb("config", HelpText = "Configurate authentication")]
    class ConfigSubOptions
    {
        [Option("username", HelpText = "Specify user name")]
        public string Username { get; set; }

        [Option("password", HelpText = "Specify password")]
        public string Password { get; set; }
    }   
}
