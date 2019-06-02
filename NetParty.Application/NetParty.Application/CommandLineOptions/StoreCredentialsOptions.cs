#region Using

using CommandLine;

#endregion

namespace NetParty.Application.CommandLineOptions
    {
    [Verb("storeCredentials", HelpText = "Store credentials used to authenticate to API.")]
    public class StoreCredentialsOptions
        {
        [Option('u', "username", Required = true)]
        public string Username { get; set; }

        [Option('p', "password", Required = true)]
        public string Password { get; set; }
        }
    }
